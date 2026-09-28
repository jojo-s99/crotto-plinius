using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;
using Microsoft.AspNetCore.Authorization;

namespace CrottoPlinius.Pages.Admin.Dishes;
[Authorize]
public class IndexModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public IndexModel(RestaurantDbContext context)
    {
        _context = context;
    }

    public List<MenuCategory> Categories { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? SelectedCategoryId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchQuery { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.SortOrder)
            .Include(c => c.Dishes.OrderBy(d => d.SortOrder))
            .AsQueryable();

        if (SelectedCategoryId.HasValue)
        {
            query = query.Where(c => c.Id == SelectedCategoryId.Value);
        }

        var list = await query.ToListAsync();

        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            var term = SearchQuery.Trim().ToLower();
            foreach (var cat in list)
            {
                cat.Dishes = cat.Dishes
                    .Where(d => d.Name.ToLower().Contains(term) || (d.Description != null && d.Description.ToLower().Contains(term)))
                    .ToList();
            }
            list = list.Where(c => c.Dishes.Any()).ToList();
        }

        Categories = list;
    }
}
