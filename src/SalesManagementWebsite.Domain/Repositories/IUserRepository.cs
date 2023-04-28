using SalesManagementWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace SalesManagementWebsite.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUser(string userName);
    }
}
