﻿using System.ComponentModel.DataAnnotations;

namespace EntityFramework_Slider.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get;set; }
        public bool RememberMe { get; set; } = false;
    }
}
