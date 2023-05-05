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
        private IItemRepository? _itemRepository;
        private ICategoryRepository? _categoryRepository;
        private IBrandRepository? _brandRepository;

        public UnitOfWork(SalesManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_dbContext); }
        }

        public IItemRepository ItemRepository
        {
            get { return _itemRepository = _itemRepository ?? new ItemRepository(_dbContext); }
        }

        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository = _categoryRepository ?? new CategoryRepository(_dbContext); }
        }

        public IBrandRepository BrandRepository
        {
            get { return _brandRepository = _brandRepository ?? new BrandRepository(_dbContext); }
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
