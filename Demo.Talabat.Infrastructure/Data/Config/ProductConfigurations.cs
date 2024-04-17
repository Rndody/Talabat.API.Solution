using Demo.Talabat.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure.Data.Config
{
	internal /*we'll only use it inside this layer*/  class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{

			//Configure the price decimal to pervent warning
			//other properties are string make sure they are requiered 
			//the string by convention will be max so handle the name to a limited chars number
			//handle the relationships [remember we only have Navigational property in the Product class model]

			builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
			builder.Property(P => P.Description).IsRequired();
			builder.Property(P => P.PictureUrl).IsRequired();
			builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

			builder.HasOne(P => P.Brand)//the product only related to one brand
					   .WithMany(/*B => B.Products*/)// we don't have Navigational property in the brand for the products, so we won't use the lambda expression
					   .HasForeignKey(P => P.BrandId)//remember we changed the Column name from ProductBrandId to BrandId, so we have to mention it as a FK here
					  /*.OnDelete(DeleteBehavior.SetNull)*/; //since the FKs are int --> requiered --> the on delete rule will be cascade, if we need to change it

			builder.HasOne(P => P.Category)//the product only related to one Category
				.WithMany()//the category has many products
			.HasForeignKey(P => P.CategoryId);//remember we changed the Column name from ProductCategoryId to CategoryId, so we have to mention it as a FK here

		}
	}
}
