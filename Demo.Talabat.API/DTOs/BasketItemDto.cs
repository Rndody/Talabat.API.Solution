using System.ComponentModel.DataAnnotations;

namespace Demo.Talabat.API.DTOs
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string ProductName { get; set; }

		[Required]
		public string PictureUrl { get; set; }

		[Required]
		[Range(0.1, double.MaxValue, ErrorMessage ="price must be greated than 0")]
		public decimal Price { get; set; }

		[Required]
		public string Category { get; set; }

		[Required]
		public string Brand { get; set; }


		[Required]
		[Range (1,int.MaxValue, ErrorMessage ="quentity must be at least one item")]
		public int Quantity { get; set; }
	}
}