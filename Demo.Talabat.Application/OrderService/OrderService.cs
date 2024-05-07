﻿using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Core.Services.Contract;
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
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<DeliveryMethod> deliveryMethodRepo;
        private readonly IGenericRepository<Order> orderRepo;

        public OrderService(IBasketRepository basketRepo, IGenericRepository<Product> productRepo, IGenericRepository<DeliveryMethod> deliveryMethodRepo, IGenericRepository<Order> orderRepo)
        {
            this.basketRepo = basketRepo;
            this.productRepo = productRepo;
            this.deliveryMethodRepo = deliveryMethodRepo;
            this.orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(basketId);
            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity)
                orderItems.Add(orderItem);
                }
            }

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            //var deliveryMethod = await deliveryMethodRepo.GetAsync(deliveryMethodId);

            var order = new Order(

                buyerEmail: buyerEmail,
                shippingAddress: shippingAddress,
                deliveryMethodId: deliveryMethodId,
                items: orderItems,
                subtotal: subtotal
                );
            orderRepo.Add(order);

        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
        public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

    }
}
