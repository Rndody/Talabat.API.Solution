using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    { //will be mapped as table in database
        private Order() { }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod? deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } = null!;

       // public int? DeliveryMethodId { get; set; }//FK
        #region //Navigational Properties
        public virtual DeliveryMethod? DeliveryMethod { get; set; }//Navigational Property [ONE]
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();//Navigational Property [Many] 
        #endregion
        public decimal Subtotal { get; set; } //the price of each item * the quantity ---> the total price of the items without delivery costs
        //[NotMapped]
        //public decimal Total  => Subtotal + DeliveryMethod.Cost; //readonly property  {get}

        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost; //getter method
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
