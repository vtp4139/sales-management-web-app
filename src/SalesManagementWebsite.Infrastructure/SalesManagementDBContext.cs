using Microsoft.EntityFrameworkCore;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Infrastructure
{
    public class SalesManagementDBContext : DbContext
    {
        public SalesManagementDBContext(DbContextOptions<SalesManagementDBContext> options) : base(options) { }

        //User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder
        //.UseLazyLoadingProxies();
    }
}
