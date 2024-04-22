using Demo.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Specifications.Product_Specs
{
	public class ProductsWithFilterationForCountSpecifications : BaseSpecifications<Product>
	{
        public ProductsWithFilterationForCountSpecifications(ProductSpecParams specParams):
            base(P=>

			(!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value) &&
			(!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)


				)
		{
            
        }
  
	
	
	}
}
