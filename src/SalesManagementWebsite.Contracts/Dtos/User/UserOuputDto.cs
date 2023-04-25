
namespace SalesManagementWebsite.Contracts.Dtos.User
{
    public class UserOuputDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IdentityCard { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Token { get; set; } = string.Empty;
        public List<UserRoleDto> Roles { get; set; } = new List<UserRoleDto>();
    }
}
