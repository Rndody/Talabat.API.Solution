using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Services.Contract
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderStatus(string paymentIntent, bool isPaid);

    }
}
