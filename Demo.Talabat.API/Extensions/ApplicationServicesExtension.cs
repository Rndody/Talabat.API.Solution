using Demo.Talabat.API.Helpers;
using Demo.Talabat.Application.AuthService;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Core.Services.Contract;
using Demo.Talabat.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // setting the default schema with the 3rd overlad
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;// any endpoint uses [Authorize] will use this schema
            })
               .AddJwtBearer(/*"Bearer",*/ options => //takes action of one parameter [JWTBearerOptions]---> authentication handler 
               {
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = configuration["JWT:ValidIssuer"],
                       ValidateAudience = true,
                       ValidAudience = configuration["JWT:ValidAudience"],
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero,
                   };
               });
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            return services;
        }



    }
}
