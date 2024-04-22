using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity  //make it generic as we don't know the query will run against which DbSet 
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> specs)
		{
			var query = inputQuery; /*dbContext.Set<Products>()*/

			if (specs.Criteria != null)
				query = query.Where(specs.Criteria); // the criteria holds the lambda expression P=>P.Id
			if (specs.OrderBy !=null)
				query = query.OrderBy(specs.OrderBy);
			else if (specs.OrderByDesc !=null) 
				query=query.OrderByDescending(specs.OrderByDesc);
				//query= dbContext.Set<Product>().Where(P=>P.Id);
				//Includes ==> list has 2 emelents
				//1--P=>P.Brand
				//2--P=>P.Category

				query = specs.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression)); //put the query returned in the query variable [this is the query builder ]

			// dbContext.Set<Product>().Where(P=>P.Id) ==> seed [query]
			//1st iteration ----> dbContext.Set<Product>().Where(P=>P.Id) .Include(P=>P.Brand)
			//-------------------[ -----------------seed --------------------- + includeExpression]  ==> will be put in the currentQuery

			//2nd iteration ------->dbContext.Set<Product>().Where(P=>P.Id) .Include(P=>P.Brand).Include(P=>P.Category)
			//---------------------------[ -----------------currentQuery --------------------- ---------------------+ includeExpression] ==> will be put in the currentQuery

			return query;
		}


		//IQueryable [return query]
		//if we use IEnumerable the filtration will occure here in the application after getting all the data 
		//we need this method to build the query and return the query 
		//the queries will be deferred execution

	}
}
