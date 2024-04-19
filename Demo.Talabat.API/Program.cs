
using Demo.Talabat.API.Helpers;
using Demo.Talabat.API.Middlewares;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Infrastructure;
using Demo.Talabat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.Talabat.API
{
	public class Program
	{
		///Entry Point of the application ---> Main 
		public static async Task Main(string[] args)
		{

			var webApplicationBuilder = WebApplication.CreateBuilder(args); //the builder that will create our web application
			/*When we create the webApplicationBuilder --> start to configure 
			 *We Configure the webApplicationBuilder with 7 things 
		   	 *The Srvices is one of them*/

			#region Configure Service
			// Add services to the container. Register the services to the Dependency Injection Container

			webApplicationBuilder.Services.AddControllers();// Register Services requierd by APIs

			#region Register Swagger Services in the DI Container
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
			#endregion

			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(Options =>
			///AddDbContext method is in the  Microsoft.EntityFrameworkCore.SqlServer package which we installed in the infrastructure layer 
			///make reference to the infrastructure layer so that we can use the package here, also we'll need to make the reference so that we can refere to our DbContext class
			{  ///how to get the connection string from the appsettings file?
			   ///in .net 5 in the StartUp	class we used to have Configuration property 
			   ///now the webApplicationBuilder object has Configuration property --> from type ConfigurationManager
				Options.UseLazyLoadingProxies().UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});

			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			/*instead of adding each domain model in a separate line
			 * [when asking for creating object from Interface IGenericRepository<Product> --> create object from class GenericRepository<Product> ]
			*we can use the 2nd overload of the AddScoped method that determines the lifetime of the object, 
			*the 2nd overload----> when asking for creating object from IGenericRepository<> of type ... create object from GenericRepository<> of that type*/

			//	webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles())); //----------old way , use it in case we have more than one profile
			webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));   //use the other overload
			#endregion

			var app = webApplicationBuilder.Build();  //the Kestrel 

			#region Update Database and appling all un-applied migrations
			//------------ 1 add scope 
			using var scope = app.Services.CreateScope(); //using --> to dispose the scope after we finish using the object
														  //the scope is the lifetime of the object we need to create we should call this method as we are asking the CLR Explicitly
														  //------------------ 2 add services
			var services = scope.ServiceProvider;
			//---------------------- 3 add our DbContext 
			var dbContext = services.GetRequiredService<ApplicationDbContext>();

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();//ask the CLR to create object from type ILoggerFactory
																			  //Note: we allowed the DI for the ILoggerFactory in when we added the AddControllers in the Services DI 
																			  //ILoggerFactory uses a design pattern [ Abstract Factory ]
			try
			{   ///now we can use the dbContext object and add the migrations and update the database
				await dbContext.Database.MigrateAsync();
				//after appling the migration add the data in the data seed 
				//remember the ApplicationDbContextSeed is static class with static method , the method takes object from ApplicationDbContext class
				await ApplicationDbContextSeed.SeedAsync(dbContext);
			}
			catch (Exception ex)
			{
				//use a ready made service to display the error msg in a presentable way ---> loggerFactory 
				var logger /*the place we will save the logs in*/= loggerFactory.CreateLogger<Program  /*we need to log the exceptions happens in the program class*/>();
				logger.LogError(ex, "we've got an error while appling the migration");
			}

			/*	ApplicationDbContext dbContext = new ApplicationDbContext();
				//we don't have parameter less constructor 
				//using the other constructor depends on creating object from DbContextOptions --> which CLR will create 
				//we have 2 options here -->1- create parameter less constructor and make it chain on the other constructor 
				//2- use the DI and ask the CLR 
				 await dbContext.Database.MigrateAsync();*/
			#endregion

			#region Configure Kestrel Middelware
			// Configure the HTTP request pipeline. // determine the middleware of the app // NOTE: the middleware must be in order
			app.UseMiddleware<ExceptionMiddleware>(); //creating object will call the constructor

			if (app.Environment.IsDevelopment())
			{//Document our API // no need to Document API in Production phase as it will be deployed on server and consumed by the frontend/mobile developer
				app.UseSwagger();
				app.UseSwaggerUI();
				//app.UseDeveloperExceptionPage();  //called internally by default after .net 5
			}
			app.UseStatusCodePagesWithReExecute("/errors/{0}"); //will be executed in case the request sent doesn't match any of our endpoints

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.MapControllers(); //reads the route of the controller from the controller Attribute Decorator
			#endregion

			app.Run();
		}
	}
}
