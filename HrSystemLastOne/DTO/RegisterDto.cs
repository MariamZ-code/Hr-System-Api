﻿using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.DTO
{
    public class RegisterDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Role { get; set; }
    }
}
