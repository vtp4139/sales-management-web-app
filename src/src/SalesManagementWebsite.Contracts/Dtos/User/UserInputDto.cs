﻿using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Contracts.Dtos.User
{
    public class UserInputDto
    {
        [Required(ErrorMessage = "{0} is empty!")]
        public string UserName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "{0} not over 100 char")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string IdentityCard { get; set; } = string.Empty;
        public int StatusUser { get; set; }

        public DateTime DOB { get; set; }


    }
}
