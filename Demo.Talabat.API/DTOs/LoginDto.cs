﻿using System.ComponentModel.DataAnnotations;

namespace Demo.Talabat.API.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }=null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;

    }
}
