using Demo.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

	}
}
