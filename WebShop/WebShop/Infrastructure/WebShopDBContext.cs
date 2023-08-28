using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Context
{
    public class WebShopDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public WebShopDBContext(DbContextOptions<WebShopDBContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebShopDBContext).Assembly);
        }
    }
}