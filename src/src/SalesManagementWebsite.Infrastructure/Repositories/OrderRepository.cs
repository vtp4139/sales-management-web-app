using Microsoft.EntityFrameworkCore;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(SalesManagementDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Order?> GetOrderAsync(Guid id)
        {
            return await _dbContext.Orders
                         .AsNoTracking()                        
                         .Include(od => od.User)
                         .Include(od => od.Customer)
                         .Include(od => od.OrderDetails)
                         .ThenInclude(odt => odt.Item)
                         .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
    }
}
