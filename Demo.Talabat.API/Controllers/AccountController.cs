using Demo.Talabat.API.DTOs;
using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Talabat.API.Controllers
{
    public class AccountController : BaseApiController
    {
        #region Fields
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        #endregion

        #region Constructors
        public AccountController(  //ask the clr to create object from UserManager & SignInManager
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #endregion

        #region Endpoints
        #region Login

        [HttpPost("login")]//post:/api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)//we need the Dto to return 3 pecies of information 1-displayname 2-email 3-token---> create UserDto
                                                                      //the LoginDto has only 2 properties 1-email 2-password
        {
            //make sure that the email is already registered in the Database ---> try to get the user with the email sent, if the email is registered then it will return the user
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "tokennn" //will handle this later when we use the JWT
            });
        }

        #endregion

        #endregion
    }
}
