﻿using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Dtos
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
