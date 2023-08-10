using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Hangfire.Services
{
    public interface IHangfireServices
    {
        Task<ResponseHandle<OrderListOutputDto>> GetOrders();
    }
}
