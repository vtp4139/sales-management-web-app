using System.Collections.ObjectModel;

namespace SalesManagementWebsite.API.Services.JWTServices
{
    public class SecurityRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";

        public static readonly IList<string> Roles = new ReadOnlyCollection<string>
        (new List<string>
            {
                Admin,
                Manager,
                User
            });
    }
}
