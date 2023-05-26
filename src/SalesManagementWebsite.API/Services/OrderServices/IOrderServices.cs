using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;


namespace SalesManagementWebsite.API.Services.OrderServices
{
    public interface IOrderServices
    {
        ValueTask<ResponseHandle<OrderListOutputDto>> GetAllOrders();
        ValueTask<ResponseHandle<OrderOutputDto>> GetOrder(Guid id);
    }
}
