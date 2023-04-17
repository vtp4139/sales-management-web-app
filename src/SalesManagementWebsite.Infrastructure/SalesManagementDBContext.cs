using Microsoft.EntityFrameworkCore;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Infrastructure
{
    public class SalesManagementDBContext : DbContext
    {
        public SalesManagementDBContext(DbContextOptions<SalesManagementDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
