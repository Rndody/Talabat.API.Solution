using Demo.Talabat.Core;
using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Core.Services.Contract;
using Demo.Talabat.Core.Specifications.Order_Specs;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Product = Demo.Talabat.Core.Entities.Product.Product;

namespace Demo.Talabat.Application.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepo;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepo = basketRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            //install the stripe in the Application layer
            StripeConfiguration.ApiKey = configuration["Publishablekey:Secretkey"];

            var basket = await basketRepo.GetBasketAsync(basketId);
            if (basket == null) return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
                basket.ShippingPrice = shippingPrice;
            }
            if (basket.Items?.Count > 0)
            {
                var productRepo = unitOfWork.Repository<Product>(); //use aliase name for Product as VS is confused
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;

                }
            }

            PaymentIntent paymentIntent;
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //create new payment intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await paymentIntentService.CreateAsync(options);//integration with stripe
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // update existing Payement intent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await basketRepo.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdateOrderStatus(string paymentIntentId, bool isPaid)
        {
            var orderRepo = unitOfWork.Repository<Order>();
            var spec = new OrderWithPaymentIntentSpecification(paymentIntentId);
            var order = await orderRepo.GetByIdWithSpecAsync(spec);

            if (order == null) return null;
            if (isPaid) order.Status = OrderStatus.PaymentReceived;
            else order.Status = OrderStatus.PaymentFailed;

            orderRepo.Update(order);
            await unitOfWork.CompleteAsync();   

            return order;
        }
    }
}
