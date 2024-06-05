using Demo.Talabat.Core;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Core.Services.Contract;
using Demo.Talabat.Core.Specifications.Order_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Application.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IUnitOfWork unitOfWork;

        //private readonly IGenericRepository<Product> productRepo;
        //private readonly IGenericRepository<DeliveryMethod> deliveryMethodRepo;
        //private readonly IGenericRepository<Order> orderRepo;

        public OrderService(IBasketRepository basketRepo,
                                        //IGenericRepository<Product> productRepo,
                                        //IGenericRepository<DeliveryMethod> deliveryMethodRepo,
                                        //IGenericRepository<Order> orderRepo
                                        IUnitOfWork unitOfWork)
        {
            this.basketRepo = basketRepo;
            this.unitOfWork = unitOfWork;
            //this.productRepo = productRepo;
            //this.deliveryMethodRepo = deliveryMethodRepo;
            //this.orderRepo = orderRepo;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(basketId);
            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0)
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var order = new Order(
                                        buyerEmail: buyerEmail,
                                        shippingAddress: shippingAddress,
                                        deliveryMethod: deliveryMethod,
                                        items: orderItems,
                                        subtotal: subtotal);
            unitOfWork.Repository<Order>().Add(order);
            var result = await unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var ordersRepo = unitOfWork.Repository<Order>();

            var spec = new OrderSpecifications(buyerEmail);
            var orders = await ordersRepo.GetAllWithSpecAsync(spec);
            return orders;
        }
        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        /* {
             var orderRepo = unitOfWork.Repository<Order>();
             var orderSpec = new OrderSpecifications(orderId, buyerEmail);
             var order = orderRepo.GetByIdWithSpecAsync(orderSpec);
             return order;
         }*/  => await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(new OrderSpecifications(orderId, buyerEmail));
      
        
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
                  => await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

    }
}
