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
		#region Constructors
		//the parameter-less constructor here chains on the base's parameter-less constructor
		public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
			: base(P =>

			(!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value) &&
			(!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)

			)
		{
			AddIncludes();
			if (!string.IsNullOrEmpty(specParams.Sort))
			{
				switch (specParams.Sort)
				{
					case "priceAsc":
						AddOrderBy(P => P.Price); break;
					case "priceDesc":
						AddOrderByDesc(P => P.Price); break;
					default: AddOrderBy(P => P.Name); break;

				}
			}
			else AddOrderBy(P => P.Name);
			ApplyPagination((specParams.PagIndex - 1 * specParams.PageSize), specParams.PageSize);
		}
		//now it will execite the code of the base constructor  in which it sets the Includes property with empty list
		//now  add the product specific Includes expressions  needed[the brand and category]

		public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id)
		=> AddIncludes();
		#endregion

		#region Methods
		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}
		#endregion

	}
}
