using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Entities.Product
{
	public class Product : BaseEntity
	{
		#region Properties
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string PictureUrl { get; set; } = null!;
		public decimal Price { get; set; }


		#region Forigen Keys Properties
		//since the relation between brand and product is one to many --> take the PK from the one to the many
		//we need to make property for the FK
		//we need to change the FK name ProductBrandId to be BrandId 
		///[ForeignKey(nameof(Brand))] // put the name of the Navigational Property --->1
		//or use the Fluent API to specify the FK that has different name   --->2
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		#endregion

		#endregion

		#region Navigational Properties		
		public virtual ProductBrand Brand { get; set; } = null!;
		public virtual ProductCategory Category { get; set; } = null!; 
		#endregion
	}
}
