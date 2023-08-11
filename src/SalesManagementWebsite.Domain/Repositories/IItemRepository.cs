using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Domain.Repositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<Item?> GetItemById(Guid id);
    }
}
