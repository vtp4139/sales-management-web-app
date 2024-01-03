
namespace SalesManagementWebsite.Contracts.Dtos.Item
{
    public class ItemListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
