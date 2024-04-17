using Demo.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Repositories.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity //repositories run against Domain Models /Domain Entities 
	{
		Task<T?> GetAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
	}
}
