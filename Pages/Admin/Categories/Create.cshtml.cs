using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Categories;

public class CreateModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public CreateModel(RestaurantDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public MenuCategory Category { get; set; } = new();

    public IActionResult OnGet()
    {
        // Suggest the next sort order
        var maxSort = _context.Categories.Select(c => (int?)c.SortOrder).Max() ?? 0;
        Category.SortOrder = maxSort + 1;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Categories.Add(Category);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
