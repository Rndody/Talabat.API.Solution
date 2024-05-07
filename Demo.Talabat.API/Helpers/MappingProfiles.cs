using AutoMapper;
using Demo.Talabat.API.DTOs;
using Demo.Talabat.Core.Entities;
//using Demo.Talabat.Core.Entities.Identity;
using Demo.Talabat.Core.Entities.Order_Aggregate;
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

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                            .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                            .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                            .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl));




        }
    }
}
