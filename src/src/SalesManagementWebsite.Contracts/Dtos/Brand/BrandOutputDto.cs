
namespace SalesManagementWebsite.Contracts.Dtos.Brand
{
    public class BrandOutputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
