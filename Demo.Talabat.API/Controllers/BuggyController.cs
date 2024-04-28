using Demo.Talabat.API.Errors;
using Demo.Talabat.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{

	public class BuggyController : BaseApiController
	{
		private readonly ApplicationDbContext dbContext;
		public BuggyController(ApplicationDbContext dbContext) //talk to the database directly 		
		=> this.dbContext = dbContext;

		//----------------------------------------------------------------------------
		[HttpGet("notfound")]// GET :api/buggy/notfound
		public ActionResult GetNotFoundRequest()
		{
			var product = dbContext.Products.Find(100);
			if (product == null) return NotFound(new ApiResponse(404));
			return Ok(product);
		}
		//----------------------------------------------------------------------------
		[HttpGet("servererror")] //GET :api/buggy/servererror
		public ActionResult GetServerError()
		{
			var product = dbContext.Products.Find(100);//null we don't have prodct 100
			var productToReturn = product.ToString();// can't make the null toString [server error]
			return Ok(productToReturn);
		}
		//----------------------------------------------------------------------------
		[HttpGet("badrequest")]//GET : api/buggy/badrequest
		public ActionResult GetBadRequest()
			=> BadRequest(new ApiResponse(400));
		//----------------------------------------------------------------------------
		[HttpGet("badrequest/{id}")] //GET : api/buggy/badrequest/five		
		public ActionResult GetBadRequest(int id) //validation error [need int and send string]
			=> Ok();
		[HttpGet("unauthorized")] //GET : api/bugy/unauthorized
		public ActionResult GetUnauthorizedError() => Unauthorized(new ApiResponse(401));
	}
}
