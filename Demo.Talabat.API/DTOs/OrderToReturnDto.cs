using Demo.Talabat.Core.Entities.Order_Aggregate;

namespace Demo.Talabat.API.DTOs
{
    public class OrderToReturnDto
    {

        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; } = null!;



        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public virtual ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;



    }
}
