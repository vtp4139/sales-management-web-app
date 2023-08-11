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
        private IUserRoleRepository? _userRoleRepository;
        private ICustomerRepository? _customerRepository;
        private IOrderRepository? _orderRepository;
        private IOrderDetailRepository? _orderDetailRepository;
        private ISupplierRepository? _supplierRepository;
        private IRoleRepository? _roleRepository;

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

        public IUserRoleRepository UserRoleRepository
        {
            get { return _userRoleRepository = _userRoleRepository ?? new UserRoleRepository(_dbContext); }
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _customerRepository = _customerRepository ?? new CustomerRepository(_dbContext); }
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository = _orderRepository ?? new OrderRepository(_dbContext); }
        }

        public IOrderDetailRepository OrderDetailRepository
        {
            get { return _orderDetailRepository = _orderDetailRepository ?? new OrderDetailRepository(_dbContext); }
        }

        public ISupplierRepository SupplierRepository
        {
            get { return _supplierRepository = _supplierRepository ?? new SupplierRepository(_dbContext); }
        }

        public IRoleRepository RoleRepository
        {
            get { return _roleRepository = _roleRepository ?? new RoleRepository(_dbContext); }
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
