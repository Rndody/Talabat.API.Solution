using Demo.Talabat.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Demo.Talabat.API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser?> FindUserWithAddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.NormalizedEmail == email.ToUpper());
            return user;
        }
    }
}
