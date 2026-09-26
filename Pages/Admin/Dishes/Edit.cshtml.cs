using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Dishes;

public class EditModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public EditModel(RestaurantDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Dish Dish { get; set; } = default!;

    public SelectList CategoryList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dish = await _context.Dishes.FirstOrDefaultAsync(m => m.Id == id);
        if (dish == null)
        {
            return NotFound();
        }

        Dish = dish;

        var categories = await _context.Categories.OrderBy(c => c.SortOrder).ToListAsync();
        CategoryList = new SelectList(categories, "Id", "Name", Dish.MenuCategoryId);

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

        _context.Attach(Dish).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Dishes.Any(e => e.Id == Dish.Id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToPage("./Index");
    }
}
