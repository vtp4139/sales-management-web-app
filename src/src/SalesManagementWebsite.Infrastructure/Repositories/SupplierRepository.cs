using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Infrastructure.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(SalesManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
