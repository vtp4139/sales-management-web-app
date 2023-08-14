using SalesManagementWebsite.Contracts.Dtos.Customer;

namespace SalesManagementWebsite.Contracts.Dtos.Order
{
    public class OrderOutputDto
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public UserOrderDto? User { get; set; }
        public CustomerOuputDto? Customer { get; set; }
        public List<OrderDetailOutputDto> OrderDetails { get; set; } = new List<OrderDetailOutputDto>();
    }

    public class UserOrderDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
