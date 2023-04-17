using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
