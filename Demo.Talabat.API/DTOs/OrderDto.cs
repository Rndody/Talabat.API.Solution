using Demo.Talabat.Core.Entities.Order_Aggregate;
using System.ComponentModel.DataAnnotations;

namespace Demo.Talabat.API.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethdId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
