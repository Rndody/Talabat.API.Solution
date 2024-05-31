using AutoMapper;
using Demo.Talabat.API.DTOs;
using Demo.Talabat.API.Errors;
using Demo.Talabat.API.Helpers;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Core.Services.Contract;
using Demo.Talabat.Core.Specifications;
using Demo.Talabat.Core.Specifications.Product_Specs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
	public class ProductsController : BaseApiController
	{
        private readonly IProductService productService;
        #region Fields
        //private readonly IGenericRepository<Product> productRepo;
        //private readonly IGenericRepository<ProductBrand> brandsRepo;
        //private readonly IGenericRepository<ProductCategory> categoriesRepo;
        private readonly IMapper mapper;
		#endregion
		//ask the CLR to create object from class implements the IGenericRepository [develop against interface not concrete class]
		//remember we don't have ProductRepository so we'll use the GenericRepository<Product>
		#region Constructors
		public ProductsController(
			IProductService productService,
			//IGenericRepository<Product> productRepo,
			//IGenericRepository<ProductBrand> brandsRepo, 
			//IGenericRepository<ProductCategory> categoriesRepo, 
			IMapper mapper) //ask the CLR in the Constructor impliicitly 
																																															//remember to register the GenericRepository<Product> object in the DI Container 
																																															//remember to register the IMapper in the DI container --> in the container we need to add our profile 
		{
            this.productService = productService;
            //this.productRepo = productRepo;
            //this.brandsRepo = brandsRepo;
            //this.categoriesRepo = categoriesRepo;
            this.mapper = mapper;
		}
		#endregion

		#region Endpoints
		//4 endpoints 
		//------------------------------ First Endpoints ----------------------------------------
		//[Authorize   (AuthenticationSchemes =/*"Bearer"*/ JwtBearerDefaults.AuthenticationScheme)] //we specified the DefaultChallengeScheme in the Main()
		// [Authorize]
		[Cached(600)]
		[HttpGet] //Get: /api/products
		//public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams ) 
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams ) //we won't use the name of the method in the routing as we used to do in the MVC
		{
			///to use the GetAllWithSpecAsync we need object from class implements ISpecifications 
			///create object from class BaseSpecifications and send it to the method
			///	var spec = new BaseSpecifications<Product>(); //we used the constructor that don't have criteria 
			///now we have a problem ---> includes are empty list while we have 2 include expression
			//	var spec = new ProductWithBrandAndCategorySpecifications(specParams);

			//	var products = await productRepo. /*GetAllAsync*/GetAllWithSpecAsync(spec);
			var products = await productService.GetProductsAsync(specParams); 
            #region special type classes 
            //	JsonResult result = new JsonResult(products);
            /*the JsonResult inherits ActionResult and implements IActionResult
			 so we can use it as a return type for the endpoint
			but the JsonResult won't return the status of the request [the status code]
			so we can use the OkObjectResult instead of JsonResult or get the status code of the result  --> */
            //result.StatusCode = 200; 
            #endregion

            var count = await productService.GetCountAsync(specParams);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
			//var countSpec = new ProductsWithFilterationForCountSpecifications(specParams);
		//	var count= await productRepo.GetCountAsync(/*spec*/countSpec);
			return Ok( /* mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products)  */    new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize,count,data)     );
		}
		//------------------------------ Second Endpoints ----------------------------------------
		#region improvment for swagger documentation
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] 
		#endregion
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			///this endpoint takes parameter [id] ... but our class ProductWithBrandAndCategorySpecifications has only parameter-less constructor
			///we need to add another constructor to send it the Id and use it in the Criteria property 
			//var spec = new ProductWithBrandAndCategorySpecifications(id);
		//	var product = await productRepo/*.GetAsync(id)*/.GetByIdWithSpecAsync(spec);
            var product = await productService.GetProductAsync(id);
			if (product == null) return NotFound(new ApiResponse(404));
			return Ok(mapper.Map<Product, ProductToReturnDto>(product));
		}

		//------------------------------ Third  Endpoints ----------------------------------------

		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
		//	var brands=await brandsRepo.GetAllAsync();
			var brands=await productService.GetBrandsAsync();
            return Ok(brands);
		}

		//------------------------------ Fourth Endpoints ----------------------------------------

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()

		{
			//var categories = await categoriesRepo.GetAllAsync();
            var categories = await productService.GetCategoriesAsync();
			return Ok(categories);
		}







		#endregion
	}
}
