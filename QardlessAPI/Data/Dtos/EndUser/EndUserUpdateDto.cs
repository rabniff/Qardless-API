﻿using System.ComponentModel.DataAnnotations;

namespace QardlessAPI.Data.Dtos.EndUser
{
    public class EndUserUpdateDto
    {
        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool EmailVerified { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? PhoneMobile { get; set; }

        public bool? PhoneMobileVerified { get; set; }

        public string? PhoneHome { get; set; }

        [Required]
        public string AddressCode { get; set; }

        [Required]
        public string AddressDetailed { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
