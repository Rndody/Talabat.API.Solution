
namespace Demo.Talabat.API.Errors
{
	public class ApiResponse
	{//class with 2 properties ...as we need to return the status code and the error msg
		public int StatusCode { get; set; }
		public string? Message { get; set; }

		public ApiResponse(int statusCode, string? message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDefaultMessageForStatusCode(statusCode);
		}
		private string? GetDefaultMessageForStatusCode(int statusCode)
		{            //if  the error msg is not send --> send the msg related to the error code 
			return statusCode switch
			{
				400 => "Bad Request",
				401 => "Unauthorized",
				404 => "Resource not found",
				500 => "errors mn 3ndena bs matgebsh sera",
				_ => null
			};
		}
	}
}
