using AutoMapper;
using Demo.Talabat.API.DTOs;
using Demo.Talabat.Core.Entities.Product;

namespace Demo.Talabat.API.Helpers
{
	public class MappingProfiles : Profile
	{
		//private readonly IConfiguration configuration;

		public MappingProfiles(/*IConfiguration configuration*/) //to read something from the appsetting we need to object from class implemnting IConfiguration
		{
			//this.configuration = configuration;
			CreateMap<Product, ProductToReturnDto>()
			.ForMember /* Distination member*/(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
			//--------------- the Brand in the DTO class is   string , will map its value from the Source [model class] go to brand object and get the string name 
			.ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name))
			//.ForMember(P=>P.PictureUrl, O=>O.MapFrom(S=>$"{configuration["ApiBaseUrl"]}/{S.PictureUrl}"));
			.ForMember(P => P.Category, O => O.MapFrom<ProductPictureUrlResolver>());

		}
	}
}
