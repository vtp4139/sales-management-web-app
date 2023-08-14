using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Infrastructure.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(SalesManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
