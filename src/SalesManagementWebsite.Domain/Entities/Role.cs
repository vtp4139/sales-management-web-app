
namespace SalesManagementWebsite.Domain.Entities
{
    public class Role : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
