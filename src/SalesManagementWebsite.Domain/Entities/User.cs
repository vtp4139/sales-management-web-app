
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
        public DateTime DOB { get; set; }
        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
