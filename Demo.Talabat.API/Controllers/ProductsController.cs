using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
	public class ProductsController : BaseApiController
	{
		#region Attributes
		private readonly IGenericRepository<Product> productRepo;
		#endregion

		//ask the CLR to create object from class implements the IGenericRepository [develop against interface not concrete class]
		//remember we don't have ProductRepository so we'll use the GenericRepository<Product>
		#region Controllers
		public ProductsController(IGenericRepository<Product> productRepo) //ask the CLR in the Constructor impliicitly 
																		   //remember to register the GenericRepository<Product> object in the DI Container 
		{ this.productRepo = productRepo; }
		#endregion



		#region Endpoints
		//2 endpoints 
		//------------------------------ First Endpoints ----------------------------------------
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts() //we won't use the name of the method in the routing as we used to do in the MVC
		{
			var products = await productRepo.GetAllAsync();
			#region special type classes 
			//	JsonResult result = new JsonResult(products);
			/*the JsonResult inherits ActionResult and implements IActionResult
			 so we can use it as a return type for the endpoint
			but the JsonResult won't return the status of the request [the status code]
			so we can use the OkObjectResult instead of JsonResult or get the status code of the result  --> */
			//result.StatusCode = 200; 
			#endregion
			return Ok(products);
		}
		//------------------------------ Second Endpoints ----------------------------------------
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await productRepo.GetAsync(id);

			if (product == null) return NotFound(new { Message = "not found", StatusCode = 404 });

			return Ok(product);
		}

		#endregion
	}
}
