using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Repositories.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository basketRepository;

		public BasketController(IBasketRepository basketRepository)
		{
			this.basketRepository = basketRepository;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustomerBasket(id));
		}


		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{

			var createdOrUpdatedBasket = await basketRepository.UpdateBasketAsync(basket);
			if (createdOrUpdatedBasket == null) return BadRequest(new ApiResponse(400));
			return Ok(createdOrUpdatedBasket);
		}

		[HttpDelete]
		public async Task DeleteBasket(string id)
		   => await basketRepository.DeleteBasketAsync(id);
	}
}
