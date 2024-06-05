using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Core.Specifications;
using Demo.Talabat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity //we need to add the T condition to the class too not only the interface
	{
		#region Fields 
		private readonly ApplicationDbContext dbContext;
		#endregion

		//to get data from database we need to create object from DbCotext ... ask the CLR
		#region Constructors
		public GenericRepository(ApplicationDbContext dbContext) //ask CLR to create object implicitly 
		{ this.dbContext = dbContext; }
		#endregion

		#region Methods
		public async Task<IReadOnlyList<T>> GetAllAsync()
		///if (typeof(T) == typeof(Product))
		///	return (IEnumerable<T>)await dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
		=> await dbContext.Set<T>().AsNoTracking().ToListAsync(); //remember AsNoTracking we only get data we won't make modification on it
																  //ToListAsync makes code Deferred 

		public async Task<T?> GetAsync(int id)
			///if (typeof(T) == typeof(Product))
			///	return await dbContext.Set<Product>().Where(P => P.Id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;
			=> await dbContext.Set<T>().FindAsync(id); //the FindAsync may return object or null ..so we need to make the T? nulable //change it in the interface also 	

		//-----------------------------With Specification 
		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		//call the method that will get the query //call it by class as it is static
		=> await /*SpecificationsEvaluator<T>.GetQuery(dbContext.Set<T>(), spec)*/ApplySpecifications(spec).AsNoTracking().ToListAsync();

		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		=> await /*SpecificationsEvaluator<T>.GetQuery(dbContext.Set<T>(), spec)*/ApplySpecifications(spec).FirstOrDefaultAsync();

		public async Task<int> GetCountAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}
		/// write the common code in a separate method ---->		
		private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
	=> SpecificationsEvaluator<T>.GetQuery(dbContext.Set<T>(), specifications);

		#endregion
	}
}
