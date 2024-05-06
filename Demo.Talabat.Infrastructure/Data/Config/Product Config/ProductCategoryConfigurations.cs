using Demo.Talabat.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure.Data.Config.Product_Config
{
	internal class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
	{
		public void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder.Property(C => C.Name).IsRequired().HasMaxLength(100);
		}
	}
}
