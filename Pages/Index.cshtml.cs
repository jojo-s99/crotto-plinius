using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages;

public class IndexModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public IndexModel(RestaurantDbContext context)
    {
        _context = context;
    }

    public List<Dish> FeaturedDishes { get; set; } = new();

    public async Task OnGetAsync()
    {
        FeaturedDishes = await _context.Dishes
            .AsNoTracking()
            .Include(d => d.Category)
            .Where(d => d.IsFeatured && d.IsAvailable && d.Category != null && d.Category.IsActive)
            .OrderBy(d => d.SortOrder)
            .Take(4)
            .ToListAsync();
    }
}
