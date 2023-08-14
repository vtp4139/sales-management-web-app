﻿using SalesManagementWebsite.Domain.Enums;

namespace SalesManagementWebsite.Domain.Entities
{
    public class User : BaseModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] Salt { get; set; } // Using to hash and compare password
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IdentityCard { get; set; } = string.Empty;
        public UserStatus UserStatus { get; set; }
        public DateTime DOB { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
