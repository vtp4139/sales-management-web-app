
namespace SalesManagementWebsite.Contracts.Dtos.Order
{
    public class OrderListOutputDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
