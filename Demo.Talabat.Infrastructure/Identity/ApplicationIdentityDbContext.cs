using Demo.Talabat.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser> //use our customized class ApplicationUser
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);//need it to apply the configurations for the 7 DbSets in the base class
            builder.Entity<Address>().ToTable("Addresses");// tell the EF that the Address class is a model class and map its name to Addresses
        }

    }
}
