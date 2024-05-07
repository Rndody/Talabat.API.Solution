using AutoMapper;
using Demo.Talabat.API.DTOs;
using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        //[ProducesResponseType(typeof(Order), StatusCode.Status200Ok)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCode.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var address = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethdId, /*orderDto.ShippingAddress*/address);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(order);
        }

    }
}
