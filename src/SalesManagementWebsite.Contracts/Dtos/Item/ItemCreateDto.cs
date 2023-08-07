
namespace SalesManagementWebsite.Contracts.Dtos.Item
{
    public class ItemCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
