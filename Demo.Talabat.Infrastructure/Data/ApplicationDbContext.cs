using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	//remember that the class DbContext is in the namespace Microsoft.EntityFrameworkCore; in the package Microsoft.EntityFrameworkCore.SqlServer
	{
		#region Constructors
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) //use the Dependency Injection way to Stablish  the connection string
			: base(options) { }
		#endregion

		#region Properties/DbSets 
		/*remember to add refernce for the Core layer to the Infrastructure layer
		 * as the we need to use the Models that are in the Core layer in the DbContext in the Infrastructure layer*/
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<ProductCategory> Categories { get; set; }


		///Order Module DbSets
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
		
		#endregion

		#region Methods

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);
		  /* we don't need to execute the base implementation as the base class doesn't have DbSets
		  * since it doesn't have DbSets, then it doesn't have special confegurations for them as they don't exist 
	      * so we'll use this method to add the fluent API configurations of our models[DbSets] using the separate class for each configuration */
		  modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //the Assembly class in the Reflection namespace
		 //the Reflection generates us code so it saves us time and effort as we write little amount of code and it generates the rest that needs to be executed
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//=> optionsBuilder.UseSqlServer("Server=. ; Database=Talabat.APIs; Trusted_Connection=True; TrustServerCertificate=true");
		/*instead of using this way for setting up the connection string , it's better to make it more dynamic by using an name for the connection string 
		 * moreover, this way the Connection String is not encrypted 
		 *Obvious for anyone can access the code [anyone can change it or see it] */

		#endregion


	}
}
