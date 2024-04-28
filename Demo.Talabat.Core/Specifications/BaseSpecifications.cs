using Demo.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Specifications
{
	public abstract class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
		//BaseEntity---> we need to build specifications that will be used to build query that we need to use against Model Entity
	{
		#region Properties
		public Expression<Func<T, bool>>? Criteria { get; set; } /*= null;*/
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }

		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; }
		#endregion

		#region Constructors
		public BaseSpecifications() { }
		public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
	   => Criteria = criteriaExpression;

		#endregion


		public void AddOrderBy(Expression<Func<T, object>> orderByExpression) { OrderBy = orderByExpression; }
		public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression) { OrderByDesc = orderByDescExpression; }


		public void ApplyPagination(int skip, int take)
		{
			IsPaginationEnabled = true;
			Skip = skip;
			Take = take;
		}


	}
}
