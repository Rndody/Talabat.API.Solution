using Demo.Talabat.API.DTOs;
using Demo.Talabat.API.Errors;
using Demo.Talabat.Core.Entities.Identity;
using Demo.Talabat.Core.Services.Contract;
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
        private readonly IAuthService authService;
        #endregion

        #region Constructors
        public AccountController(  //ask the clr to create object from UserManager & SignInManager
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IAuthService authService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authService = authService;
        }
        #endregion

        #region Endpoints
        #region Login

        [HttpPost("login")]//post:/api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)//we need the Dto to return 3 pecies of information 1-displayname 2-email 3-token---> create UserDto
                                                                      //the LoginDto has only 2 properties 1-email 2-password
        {  //make sure that the email is already registered in the Database ---> try to get the user with the email sent, if the email is registered then it will return the user
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await authService.CreateTokenAsync(user, userManager)
            });
        }

        #endregion

        #region Register
        [HttpPost("register")]//post:/api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)//the RegisterDto is what we'll recieve from frontend
        {
            //make sure that the email is not already registered before

            //build the user object from the RegisterDto without automapper
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.Phone,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(/*new ApiValidationErrorResponse() { Errors = result.Errors.Select(E => E.Description) }*/);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await authService.CreateTokenAsync(user, userManager)

            });
        }
        #endregion

        #endregion
    }
}
