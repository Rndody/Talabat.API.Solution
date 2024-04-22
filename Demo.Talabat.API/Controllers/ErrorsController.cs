using Demo.Talabat.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
	[Route("errors/{code}")] //the code is the status code
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]//telling swagger to ignore the documentation of this controller
	//this controller is related to development no need to document it
	public class ErrorsController : ControllerBase
	{
		public ActionResult Error(int code){
		
		if(code == 400) return BadRequest(new ApiResponse(400));
		else if (code==401) return Unauthorized(new ApiResponse(401));
		else if (code==404) return NotFound(new ApiResponse(code));
		else return StatusCode(code);

		}
	}
}
