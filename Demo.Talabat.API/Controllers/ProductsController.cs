using AutoMapper;
using Demo.Talabat.API.DTOs;
using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Core.Specifications;
using Demo.Talabat.Core.Specifications.Product_Specs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
	public class ProductsController : BaseApiController
	{
		#region Fields
		private readonly IGenericRepository<Product> productRepo;
		private readonly IMapper mapper;
		#endregion
		//ask the CLR to create object from class implements the IGenericRepository [develop against interface not concrete class]
		//remember we don't have ProductRepository so we'll use the GenericRepository<Product>
		#region Constructors
		public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper) //ask the CLR in the Constructor impliicitly 
																						   //remember to register the GenericRepository<Product> object in the DI Container 
																						   //remember to register the IMapper in the DI container --> in the container we need to add our profile 
		{
			this.productRepo = productRepo;
			this.mapper = mapper;
		}
		#endregion

		#region Endpoints
		//2 endpoints 
		//------------------------------ First Endpoints ----------------------------------------
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts() //we won't use the name of the method in the routing as we used to do in the MVC
		{
			///to use the GetAllWithSpecAsync we need object from class implements ISpecifications 
			///create object from class BaseSpecifications and send it to the method
			///	var spec = new BaseSpecifications<Product>(); //we used the constructor that don't have criteria 
			///now we have a problem ---> includes are empty list while we have 2 include expression
			var spec = new ProductWithBrandAndCategorySpecifications();

			var products = await productRepo. /*GetAllAsync*/GetAllWithSpecAsync(spec);
			#region special type classes 
			//	JsonResult result = new JsonResult(products);
			/*the JsonResult inherits ActionResult and implements IActionResult
			 so we can use it as a return type for the endpoint
			but the JsonResult won't return the status of the request [the status code]
			so we can use the OkObjectResult instead of JsonResult or get the status code of the result  --> */
			//result.StatusCode = 200; 
			#endregion
			return Ok(mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
		}
		//------------------------------ Second Endpoints ----------------------------------------
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			///this endpoint takes parameter [id] ... but our class ProductWithBrandAndCategorySpecifications has only parameter-less constructor
			///we need to add another constructor to send it the Id and use it in the Criteria property 
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await productRepo/*.GetAsync(id)*/.GetWithSpecAsync(spec);
			if (product == null) return NotFound(new ApiResponse(404));
			return Ok(mapper.Map<Product, ProductToReturnDto>(product));
		}
		#endregion
	}
}
