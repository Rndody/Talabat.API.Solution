using Demo.Talabat.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure.Identity
{
    public static class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        ///remember Identity package has 3 main servecies one of them is the user manager that allow me to add/update/delete user
        ///let the method take object from the UserManager<ApplicationUser>
        {
            if (!userManager.Users.Any()) //check if the database is empty go and add this user [data seeding]
            {    //create object from class ApplicationUser
                var user = new ApplicationUser()
                {
                    DisplayName = "Nader El-Salamony",
                    Email = "Rndody@gmail.com",
                    UserName = "salam",
                    PhoneNumber = "01183323487"
                };
                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
