using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Domain.Repositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<Item?> GetItem(Guid id);
    }
}
