
namespace SalesManagementWebsite.Domain.Entities
{
    public class Customer : BaseModel
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
