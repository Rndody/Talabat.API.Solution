using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
	[ApiController] //we use this attribute to let the class take the behaviour of the API controller
	[Route("[controller]")]
	// [controller] <-- the controller name , if we removed the square brakets the route will be controller "the word controller not the name of the controller"
	public class WeatherForecastController : ControllerBase
	{
		#region Attributes
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		#endregion

		#region Constructors
		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		#endregion

		#region Endpoints/Actions
		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			}).ToArray();
		} 
		#endregion
	}
}
