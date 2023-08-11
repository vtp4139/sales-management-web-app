﻿using Microsoft.EntityFrameworkCore;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Repositories;

namespace SalesManagementWebsite.Infrastructure.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(SalesManagementDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Item?> GetItemById(Guid id)
        {
            return await _dbContext.Items
                         .AsNoTracking()
                         .Include(cate => cate.Category)
                         .Include(brand => brand.Brand)
                         .Include(sup => sup.Supplier)
                         .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
    }
}
