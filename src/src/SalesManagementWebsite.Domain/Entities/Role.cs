using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Domain.Entities
{
    public class Role : BaseModel
    {
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "NVARCHAR(500)")]
        public string? Description { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
