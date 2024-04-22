using Demo.Talabat.API.Errors;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Demo.Talabat.API.Middlewares
{
	//By Convention
	public class ExceptionMiddleware /*: IMiddleware*/  //factory based way 
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;
		private readonly IWebHostEnvironment env;
		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
		//ask the clr to create objects from 
		{
			this.next = next; //reference/pointer to the next middleware
			this.logger = logger;
			this.env = env;
		}
		#region method created in the by convention way 
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				//writing code here to take action with request send
				await next.Invoke(httpContext);// go to the next middleware
											   //writing code here to take action with  returned response
			}
			catch (Exception ex)
			{
				//1st log the exception
				logger.LogError(ex.Message); //in the developlment environment
											 //in production log exception in (database or files)
											 // the user waiting the response [the response has header and body ]
											 //2nd 
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var response = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
					: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				//we need to make the json response to be in a camelcase not pascale
				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				var json = JsonSerializer.Serialize(response, options);
				await httpContext.Response.WriteAsync(json);
			}

		}

		#endregion

		#region the IMiddleware implemented method
		//public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
		//{
		//	try
		//	{
		//		//writing code here to take action with request send
		//		await next.Invoke(httpContext);// go to the next middleware
		//									   //writing code here to take action with  returned response
		//	}
		//	catch (Exception ex)
		//	{
		//		//1st log the exception
		//		logger.LogError(ex.Message); //in the developlment environment
		//									 //in production log exception in (database or files)
		//									 // the user waiting the response [the response has header and body ]
		//									 //2nd 
		//		httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		//		httpContext.Response.ContentType = "application/json";

		//		var response = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
		//			: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

		//		//we need to make the json response to be in a camelcase not pascale
		//		var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

		//		var json = JsonSerializer.Serialize(response, options);
		//		await httpContext.Response.WriteAsync(json);
		//	}
		//}


		#endregion

	}
}
