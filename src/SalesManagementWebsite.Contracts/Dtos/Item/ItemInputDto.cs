
namespace SalesManagementWebsite.Contracts.Dtos.Item
{
    public class ItemInputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
