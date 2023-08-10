using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;


namespace SalesManagementWebsite.Core.Services.OrderServices
{
    public interface IOrderServices
    {
        ValueTask<ResponseHandle<OrderListOutputDto>> GetAllOrders();
        ValueTask<ResponseHandle<OrderOutputDto>> GetOrder(Guid id);
        ValueTask<ResponseHandle<OrderOutputDto>> CreateOrder(OrderInputDto orderCreateDto);
    }
}
