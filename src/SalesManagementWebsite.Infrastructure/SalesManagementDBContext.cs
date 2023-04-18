using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Infrastructure
{
    public class SalesManagementDBContext : DbContext
    {
        public SalesManagementDBContext(DbContextOptions<SalesManagementDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
