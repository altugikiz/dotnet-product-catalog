using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Models;

namespace ProductCatalog.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    // Initial Data (Seed)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Technology" },
            new Category { Id = 2, Name = "Clothing" }
        );

        // Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "MacBook Air", Price = 45000, CategoryId = 1 },
            new Product { Id = 2, Name = "iPhone 15", Price = 60000, CategoryId = 1 },
            new Product { Id = 3, Name = "Hoodie", Price = 1500, CategoryId = 2 }
        );
    }
}