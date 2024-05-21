using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{basketid}")]// Get: /api/payments/{basketid}
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket = await paymentService.CreateOrUpdatePaymentIntent(basketid);
            if (basket == null) return BadRequest(new ApiResponse(400, "error with ur basket"));

            return Ok(basket);
        }

    }
}
