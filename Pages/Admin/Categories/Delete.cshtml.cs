using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Categories;

public class DeleteModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public DeleteModel(RestaurantDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public MenuCategory Category { get; set; } = default!;

    public int AssociatedDishesCount { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .Include(c => c.Dishes)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        Category = category;
        AssociatedDishesCount = category.Dishes.Count;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
