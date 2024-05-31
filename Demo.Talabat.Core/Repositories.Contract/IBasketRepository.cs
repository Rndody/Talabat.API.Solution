using Demo.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Repositories.Contract
{
	public interface IBasketRepository
	{
		Task <CustomerBasket?> GetBasketAsync(string basketId);
		Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
