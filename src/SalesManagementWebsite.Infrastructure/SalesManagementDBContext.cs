using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Infrastructure
{
    public class SalesManagementDBContext : DbContext
    {
        public SalesManagementDBContext(DbContextOptions<SalesManagementDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
