using EntityFramewrokCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramewrokCore.Data;

 public class AppDbContext:DbContext
    {
    public DbSet<Product> Products { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,"products.db"
                );
            options.UseSqlite($"Data Source={path}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
        }
    }
