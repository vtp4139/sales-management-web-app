using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IItemRepository ItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBrandRepository BrandRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        ISupplierRepository SupplierRepository { get; }

        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
