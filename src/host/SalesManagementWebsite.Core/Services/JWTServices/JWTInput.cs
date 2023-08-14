namespace SalesManagementWebsite.Core.Services.JWTServices
{
    public class JWTInput
    {
        public string jwtSecret { get; set; } = string.Empty;
        public string issuer { get; set; } = string.Empty;
        public string audience { get; set; } = string.Empty;
        public Guid id { get; set; }
        public string userName { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public List<string>? userRoles { get; set; }
 
    }
}
