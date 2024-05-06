using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Demo.Talabat.Core.Entities.Identity;
using Demo.Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Demo.Talabat.Application.AuthService
{
    public class AuthService : IAuthService
    {
        #region Fields
        private readonly IConfiguration configuration;
        #endregion


        #region Constructors
        public AuthService(IConfiguration configuration) //IConfiguration to deal with the appsettings
        { this.configuration = configuration; }
        #endregion

        #region Methods
        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            #region Information Exchange
            //private claims (user-defined) we'll create 2 private claims 1-name 2-email
            var authClaims = new List<Claim>()
            {
              /*new Claim("Name", user.DisplayName),//we don't send the claims names as string to avoid mis-typing and to stay consistent with the frontend
               * we use the class ClaimTypes that contain constant values for the claims
              * the frontend side also have class with the same constants   */
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)  //we used the user roles as claims and adding them to the claims 
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            #endregion

            #region Build the Security key
            //----> install the JWT package in the Application layer

            //var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StrongAuthenticationKey")); //better to put the key in the appsettings
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty));
            #endregion

            #region Registered Claims  pre defined claims
            //we'll write only 3 of them  1-audience 2-issure 3-expire
            var token = new JwtSecurityToken(   //this is the object we are going to use to build the token
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"] ?? "0")),
                claims: authClaims,    //private claims we build at the begining of the method
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );
            #endregion

            #region Buildng the token
            return new JwtSecurityTokenHandler().WriteToken(token);
            #endregion
        }
        #endregion


    }
}
