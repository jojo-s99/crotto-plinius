using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Dishes;

public class CreateModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public CreateModel(RestaurantDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Dish Dish { get; set; } = new();

    public SelectList CategoryList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? categoryId)
    {
        var categories = await _context.Categories.OrderBy(c => c.SortOrder).ToListAsync();
        CategoryList = new SelectList(categories, "Id", "Name", categoryId);

        if (categoryId.HasValue)
        {
            Dish.MenuCategoryId = categoryId.Value;
            var maxSort = await _context.Dishes
                .Where(d => d.MenuCategoryId == categoryId.Value)
                .Select(d => (int?)d.SortOrder)
                .MaxAsync() ?? 0;
            Dish.SortOrder = maxSort + 1;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var categories = await _context.Categories.OrderBy(c => c.SortOrder).ToListAsync();
            CategoryList = new SelectList(categories, "Id", "Name", Dish.MenuCategoryId);
            return Page();
        }

        _context.Dishes.Add(Dish);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
