namespace SalesManagementWebsite.Client.Paths
{
    public static class InternalAPIs
    {
        // Users
        public const string Login = "/api/users/login";
        public const string GetUser = "/api/users/{0}";

        // Categories
        public const string GetAllCategories = "/api/categories";
        public const string CreateCategory = "/api/categories";
        public const string UpdateCategory = "/api/categories/{0}";
        public const string DeleteCategory = "/api/categories/{0}";
    }
}
