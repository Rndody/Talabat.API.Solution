using AutoMapper;
using Demo.Talabat.API.DTOs;
using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi =true)]
    [Authorize /*(Policy="")*/]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var address = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethdId, /*orderDto.ShippingAddress*/address);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string email)
        {
            var orders = await orderService.GetOrdersForUserAsync(email);
            return Ok(mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }
   
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id, string email)
        {
            var order = await orderService.GetOrderByIdForUserAsync(email, id);
            if (order == null) return NotFound(new ApiResponse(404));
            return Ok(mapper.Map<OrderToReturnDto>(order));

        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods=await orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
