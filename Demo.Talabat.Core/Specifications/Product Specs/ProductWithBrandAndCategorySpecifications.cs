using Demo.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product> // we specify the type of T as this class is for Product specialy
	{
		//the parameter-less constructor here chains on the base's parameter-less constructor
		public ProductWithBrandAndCategorySpecifications() : base()
		//now it will execite the code of the base constructor  in which it sets the Includes property with empty list
		//now  add the product specific Includes expressions  needed[the brand and category]
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}

	}
}
