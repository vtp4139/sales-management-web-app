
namespace SalesManagementWebsite.Contracts.Dtos.Order
{
    public class OrderInputDto
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Guid CustomerId { get; set; }
        public List<OrderDetailInputDto> OrderDetails { get; set; } = new List<OrderDetailInputDto>();
    }
}
