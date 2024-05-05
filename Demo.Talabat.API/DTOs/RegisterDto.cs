using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.Talabat.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.\\d)(?=.[a-z]) (?=. [ [A-Z])(?=.[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
                ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; } = null!;
        /*we need the password to follow some rules ...we can write those rules in the password configurations in the --->
         * Identity configurations in the Main method---> AddIdentity options  --->
         * those configurations are executed when we create user [the create user method]
         * but we don't want reach the endpoint in case the password is invalid 
         * so we are going to write the password validations here before reaching the create user method*/
    }
}



