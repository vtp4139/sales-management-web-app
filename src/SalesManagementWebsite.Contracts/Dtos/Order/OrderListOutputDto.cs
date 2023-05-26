
namespace SalesManagementWebsite.Contracts.Dtos.Order
{
    public class OrderListOutputDto
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
