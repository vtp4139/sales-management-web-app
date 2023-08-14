
namespace SalesManagementWebsite.Contracts.Dtos.Role
{
    public class RoleUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
