using Demo.Talabat.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Demo.Talabat.API.DTOs
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }
		public List<BasketItemDto> Items { get; set; }

	}
}
