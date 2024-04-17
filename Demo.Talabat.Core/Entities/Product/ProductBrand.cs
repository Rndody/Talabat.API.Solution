using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Entities.Product
{
	public class ProductBrand : BaseEntity
	{
		#region Properties
		public string Name { get; set; } = null!;
		#endregion

		///Navigational Property
		/*public ICollection<Product> Products { get; set; } = new HashSet<Product>(); 
		 * we Initialize the property with the HashSet to prevent the null reference exception*/

		/*we don't need to know the whole products for specific brand 
	     * but if we don't mention that the relation is One-Many the EF will map it By Convention to One-One
		 * so we will make the mapping throught the configuration class [fluent API] */

	}
}
