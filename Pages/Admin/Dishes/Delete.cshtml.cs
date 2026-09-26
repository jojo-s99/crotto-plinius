using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Dishes;

public class DeleteModel : PageModel
{
    private readonly RestaurantDbContext _context;

    public DeleteModel(RestaurantDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Dish Dish { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dish = await _context.Dishes
            .Include(d => d.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (dish == null)
        {
            return NotFound();
        }

        Dish = dish;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dish = await _context.Dishes.FindAsync(id);
        if (dish != null)
        {
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
