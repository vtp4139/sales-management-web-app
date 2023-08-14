using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementWebsite.Domain.Entities
{
    public class UserRole : BaseModel
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; } 
    }
}
