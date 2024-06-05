using Demo.Talabat.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Specifications.Order_Specs
{
    public class OrderWithPaymentIntentSpecification:BaseSpecifications<Order>
    {


        public OrderWithPaymentIntentSpecification(string? paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
