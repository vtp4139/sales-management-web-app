
namespace SalesManagementWebsite.Domain.Entities
{
    public class UserRole : BaseModel
    {
        public virtual User Users { get; set; } = new User();
        public virtual Role Roles { get; set; } = new Role();
    }
}
