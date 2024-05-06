using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Entities.Order_Aggregate
{
    public enum OrderStatus
    {
        //the payment status [paid / pending ...]

       // [EnumMember(Value ="Pending")] discuss this data annotation later 
        Pending,
        PaymentReceived, 
        PaymentFailed

    }
}
