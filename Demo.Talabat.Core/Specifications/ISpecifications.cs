using Demo.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity//from type BaseEntity as we'll apply the query on a DbSet from class Model Type
	{
		//make property signature for each and every spec. we need to use
		//for now we only have 2 spec.s [where & include] --> we know from the GenericRepository class
		/*Note: the FirstOrDefault and the ToList operators will be used in the GenericRepository class directly,
		we won't add them in the Specification Properties[we only use them to make the query Deferrr execution]*/
		//in the GET by Id we need to filter by the Id [use Where] and we need to include the data of the brand and category [use include]
		//in the Get All we only need to include the data from brand and category -----> so we need 2 specs signatures 1--where  2--include 
		public Expression<Func<T, bool>>? Criteria { get; set; }//P=> P.Id==10 , takes one parameter of type T and return type boolen

		public List<Expression<Func<T, object>>> Includes { get; set; } //return object [brand/cateory/order-item/department]
																		//the parameter T ---> product/order/employee 

		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }



	}
}
