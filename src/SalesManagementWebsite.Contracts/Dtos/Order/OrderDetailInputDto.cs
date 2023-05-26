
namespace SalesManagementWebsite.Contracts.Dtos.Order
{
    public class OrderDetailOutputDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public Guid ItemId { get; set; }
    }
}
