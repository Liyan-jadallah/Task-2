using EfCoreAdvancedDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreAdvancedDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name);
       
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);
    }
}