using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity 
    {
        //this class will be table in Database

        public string ShortName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; } //delivery cost
        public string DeliveryTime { get; set; } = null!; //will be text message telling user [delivery will be within 2 days or week]


        //public ICollection<Order> Orders { get; set; } //Navigational Property [Many]
    }
}
