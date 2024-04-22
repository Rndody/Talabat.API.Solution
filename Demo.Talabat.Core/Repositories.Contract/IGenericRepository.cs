using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Repositories.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity //repositories run against Domain Models /Domain Entities 
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T?> GetAsync(int id);

		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
		Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
	}
}
