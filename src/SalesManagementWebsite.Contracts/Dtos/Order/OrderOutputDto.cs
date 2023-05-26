
namespace SalesManagementWebsite.Contracts.Dtos.Order
{
    public class OrderOutputDto
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderDetailOutputDto> OrderDetails { get; set; } = new List<OrderDetailOutputDto>();
    }
}
