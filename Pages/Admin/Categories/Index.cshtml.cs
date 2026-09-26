using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Categories;

public class IndexModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public IndexModel(RestaurantDbContext context)
    {
        _context = context;
    }

    public List<MenuCategory> Categories { get; set; } = new();

    public async Task OnGetAsync()
    {
        Categories = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Dishes)
            .OrderBy(c => c.SortOrder)
            .ToListAsync();
    }
}
