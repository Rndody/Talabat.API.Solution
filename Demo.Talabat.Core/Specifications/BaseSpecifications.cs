using Demo.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
		//BaseEntity---> we need to build specifications that will be used to build query that we need to use against Model Entity
	{
		#region Properties
		public Expression<Func<T, bool>>? Criteria { get; set; } /*= null;*/
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		#endregion

		#region Constructors
		public BaseSpecifications() { }
		public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
	   => Criteria = criteriaExpression;

		#endregion

	}
}
