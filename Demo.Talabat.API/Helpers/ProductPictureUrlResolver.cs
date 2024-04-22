using AutoMapper;
using AutoMapper.Execution;
using Demo.Talabat.API.DTOs;
using Demo.Talabat.Core.Entities.Product;

namespace Demo.Talabat.API.Helpers
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string> /*interface in auto mapper */
	{
		private readonly IConfiguration configuration;
		public ProductPictureUrlResolver(IConfiguration configuration)
			=> this.configuration = configuration;
		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			return string.Empty;
		}
	}
}
