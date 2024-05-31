using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure.Data
{
	public static class ApplicationDbContextSeed
	{
		public async static Task SeedAsync(ApplicationDbContext dbContext)// object from the DbContext as we need to add the data in the database 
		{
			if (! dbContext.Brands.Any())// Any() returns true if it has at least one element // if it is false go and make data seeding 
			{
				//start with the brand and category and let the product be the last one as it has a required FKs
				var brandsData = File.ReadAllText("../Demo.Talabat.Infrastructure/Data/DataSeed/brands.json");
				//the File class has method [ReadAllText] takes the path of the file and closes the file ... remeber the CLR won't close the file 

				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
				//at this step we Serialized from string to json --->  then Deserialized from json to ---> list of type ProductBrand
				if (brands?.Count > 0) // cheack if it is not null add in the database // use the count property in the list better than the Count () method of the LINQ
				{
					foreach (var brand in brands)
					{
						dbContext.Set<ProductBrand>().Add(brand); //set is a method in the DbContext Class 
					}
					await dbContext.SaveChangesAsync();//save changes once after finishing the loop 
				} 
			}

			if (!dbContext.Categories.Any())
			{
				var categoriesData = File.ReadAllText("../Demo.Talabat.Infrastructure/Data/DataSeed/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

				if (categories?.Count > 0)
				{
					foreach (var category in categories)
					{
						dbContext.Set<ProductCategory>().Add(category);
					}
					await dbContext.SaveChangesAsync();
				} 
			}

			if (! dbContext.Products.Any() )
			{
				var productsData = File.ReadAllText("../Demo.Talabat.Infrastructure/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products?.Count > 0)
				{
					foreach (var product in products)
					{
						dbContext.Set<Product>().Add(product);
					}
					await dbContext.SaveChangesAsync();
				} 
			}

            if (!dbContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = File.ReadAllText("../Demo.Talabat.Infrastructure/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods?.Count > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

        }
	}
}
