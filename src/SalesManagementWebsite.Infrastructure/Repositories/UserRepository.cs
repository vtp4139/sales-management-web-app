using SalesManagementWebsite.Domain;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SalesManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
