using SalesManagementWebsite.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementWebsite.Domain.Entities
{
    public class User : BaseModel
    {
        [Required]
        [Column(TypeName = "NVARCHAR(300)")]
        public required string UserName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(500)")]
        public required string Password { get; set; } 

        public required byte[] Salt { get; set; } // Using to hash and compare password

        [Required]
        public required string Name { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? Email { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? IdentityCard { get; set; }

        public UserStatus UserStatus { get; set; }

        public DateTime DOB { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
