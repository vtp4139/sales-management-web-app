using SalesManagementWebsite.Domain;
using SalesManagementWebsite.Domain.Repositories;
using SalesManagementWebsite.Domain.UnitOfWork;
using SalesManagementWebsite.Infrastructure.Repositories;

namespace SalesManagementWebsite.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesManagementDBContext _dbContext;
        private IUserRepository? _userRepository;


        public UnitOfWork(SalesManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_dbContext); }
        }


        public void Commit()
            => _dbContext.SaveChanges();


        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();


        public void Rollback()
            => _dbContext.Dispose();


        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
