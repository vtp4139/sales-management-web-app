using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Domain.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderAsync(Guid id);
    }
}
