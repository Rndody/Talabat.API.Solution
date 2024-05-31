using Demo.Talabat.API.DTOs;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using AutoMapper;

namespace Demo.Talabat.API.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{configuration["ApiBaseUrl"]}/{source.Product.PictureUrl}";
            return string.Empty;
        }
    }
}
