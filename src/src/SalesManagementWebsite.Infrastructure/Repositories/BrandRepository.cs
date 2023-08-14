using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(SalesManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
