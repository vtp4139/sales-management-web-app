
namespace SalesManagementWebsite.Domain.Entities
{
    public class Supplier : BaseModel
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
