using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Demo.Talabat.API.Controllers
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;

        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        private const string whSecret = "whsec_20494cdf9926e4c846694d295b8107fce78e6d172e1904bdda323ab251b892c2";


        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }

        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketid}")]// post: /api/payments/{basketid}
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket = await paymentService.CreateOrUpdatePaymentIntent(basketid);
            if (basket == null) return BadRequest(new ApiResponse(400, "error with ur basket"));

            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], whSecret);
            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
            Order order;
            // Handle the event
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order = await paymentService.UpdateOrderStatus(paymentIntent.Id, true);
                    logger.LogInformation("tamam {0}", order?.PaymentIntentId);
                    logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
                    break;
                case Events.PaymentIntentPaymentFailed:
                    order = await paymentService.UpdateOrderStatus(paymentIntent.Id, false);
                    logger.LogInformation("msh tamam  {0}", order?.PaymentIntentId);
                    logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);

                    break;
            }

            return Ok();

        }
    }
}
