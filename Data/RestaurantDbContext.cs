using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Models;

namespace CrottoPlinius.Data;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public DbSet<MenuCategory> Categories => Set<MenuCategory>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // MenuCategory
        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.HasIndex(e => e.SortOrder);
        });

        // Dish
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.HasIndex(e => new { e.MenuCategoryId, e.SortOrder });
            entity.HasIndex(e => e.IsAvailable);

            entity.HasOne(d => d.Category)
                  .WithMany(c => c.Dishes)
                  .HasForeignKey(d => d.MenuCategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Menu
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // AdminUser
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired();
        });
    }
}
