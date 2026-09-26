using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin;

public class IndexModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public IndexModel(RestaurantDbContext context)
    {
        _context = context;
    }

    public int TotalCategories { get; set; }
    public int ActiveCategories { get; set; }
    public int TotalDishes { get; set; }
    public int AvailableDishes { get; set; }
    public List<Dish> UnavailableDishes { get; set; } = new();

    public async Task OnGetAsync()
    {
        TotalCategories = await _context.Categories.CountAsync();
        ActiveCategories = await _context.Categories.CountAsync(c => c.IsActive);
        TotalDishes = await _context.Dishes.CountAsync();
        AvailableDishes = await _context.Dishes.CountAsync(d => d.IsAvailable);

        UnavailableDishes = await _context.Dishes
            .AsNoTracking()
            .Include(d => d.Category)
            .Where(d => !d.IsAvailable)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }
}
