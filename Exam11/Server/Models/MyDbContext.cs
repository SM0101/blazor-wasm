using Exam11.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Exam11.Server.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasData(
                new Product { ProductId = 1, ProductName = "Lenovo-102" },
                new Product { ProductId = 2, ProductName = "Hp-101" },
                new Product { ProductId = 3, ProductName = "Acer-110" }
                );
        }
    }
}
