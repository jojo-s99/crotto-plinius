using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrottoPlinius.Services;

namespace CrottoPlinius.Pages.Admin.Dishes;

public class ToggleModel : PageModel
{
    private readonly IMenuService _menuService;

    public ToggleModel(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<IActionResult> OnPostAsync(int id, string? returnUrl = null)
    {
        await _menuService.ToggleDishAvailabilityAsync(id);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToPage("./Index");
    }
}
