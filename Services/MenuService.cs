using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Services;

public interface IMenuService
{
    Task<List<MenuCategory>> GetPublicMenuAsync();
    Task<List<MenuCategory>> GetAllCategoriesWithDishesAsync();
    Task<bool> ToggleDishAvailabilityAsync(int dishId);
}

public class MenuService : IMenuService
{
    private readonly RestaurantDbContext _context;

    public MenuService(RestaurantDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Fetches all active categories with available dishes for public rendering.
    /// Uses AsNoTracking and eager loading for optimal read performance.
    /// </summary>
   public async Task<List<MenuCategory>> GetPublicMenuAsync()
{
    return await _context.Categories
        .AsNoTracking()
        .Where(c => c.IsActive)
        .OrderBy(c => c.SortOrder)
        .Select(c => new MenuCategory
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ImageUrl = c.ImageUrl, // <-- AGGIUNTO QUESTO
            SortOrder = c.SortOrder,
            IsActive = c.IsActive,
            Dishes = c.Dishes
                .Where(d => d.IsAvailable)
                .OrderBy(d => d.SortOrder)
                .ToList()
        })
        .ToListAsync();
}
    /// <summary>
    /// Fetches all categories including inactive ones and all dishes for the Admin panel.
    /// </summary>
    public async Task<List<MenuCategory>> GetAllCategoriesWithDishesAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.SortOrder)
            .Include(c => c.Dishes.OrderBy(d => d.SortOrder))
            .ToListAsync();
    }

    /// <summary>
    /// Atomic availability toggle for 1-click updates from the admin interface.
    /// </summary>
    public async Task<bool> ToggleDishAvailabilityAsync(int dishId)
    {
        var dish = await _context.Dishes.FindAsync(dishId);
        if (dish == null)
        {
            return false;
        }

        dish.IsAvailable = !dish.IsAvailable;
        await _context.SaveChangesAsync();
        return dish.IsAvailable;
    }
}
