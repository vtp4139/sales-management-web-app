using SalesManagementWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace SalesManagementWebsite.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUser(string userName);
    }
}
