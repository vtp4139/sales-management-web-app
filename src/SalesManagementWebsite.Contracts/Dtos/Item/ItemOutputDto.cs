using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Contracts.Dtos.Supplier;

namespace SalesManagementWebsite.Contracts.Dtos.Item
{
    public class ItemOutputDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public CategoryOutputDto? Category { get; set; }
        public BrandOutputDto? Brand { get; set; }
        public SupplierOutputDto? Supplier { get; set; }
    }
}
