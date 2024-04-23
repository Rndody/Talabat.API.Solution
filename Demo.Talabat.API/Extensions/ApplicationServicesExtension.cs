using Demo.Talabat.API.Helpers;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace Demo.Talabat.API.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));


			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			/*instead of adding each domain model in a separate line
			 * [when asking for creating object from Interface IGenericRepository<Product> --> create object from class GenericRepository<Product> ]
			*we can use the 2nd overload of the AddScoped method that determines the lifetime of the object, 
			*the 2nd overload----> when asking for creating object from IGenericRepository<> of type ... create object from GenericRepository<> of that type*/

			//	webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles())); //----------old way , use it in case we have more than one profile
			services.AddAutoMapper(typeof(MappingProfiles));   //use the other overload
			return services; //return the container
		}

	}
}
