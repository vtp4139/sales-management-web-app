using Microsoft.EntityFrameworkCore;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SalesManagementDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUser(string userName)
        {
           return await _dbContext.Users
                        .AsNoTracking()
                        .Include(roles => roles.UserRoles)
                        .ThenInclude(roles => roles.Roles)
                        .FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        }
    }
}
