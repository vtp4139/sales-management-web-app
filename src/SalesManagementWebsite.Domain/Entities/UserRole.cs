
namespace SalesManagementWebsite.Domain.Entities
{
    public class UserRole : BaseModel
    {
        public User Users { get; set; } = new User();
        public Role Roles { get; set; } = new Role();
    }
}
