using Demo.Talabat.Core.Entities.Product;

namespace Demo.Talabat.API.DTOs
{
	public class ProductToReturnDto
	{
		public int Id { get; set; }//write the id as we don't inherit from 	[ BaseEntity ]
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string PictureUrl { get; set; } = null!;
		public decimal Price { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		///use string to flatten the response [instead of returning the whole object, just return its name]
		public virtual string Brand { get; set; } = null!;//string instead of object from type ProductBrand
		public virtual string Category { get; set; } = null!; //string instead of object from type ProductCategory
	}
}
