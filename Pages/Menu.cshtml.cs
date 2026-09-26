using Microsoft.AspNetCore.Mvc.RazorPages;
using CrottoPlinius.Models;
using CrottoPlinius.Services;

namespace CrottoPlinius.Pages;

public class MenuModel : PageModel
{
    private readonly IMenuService _menuService;

    public MenuModel(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public List<MenuCategory> Categories { get; set; } = new();

    public async Task OnGetAsync()
    {
        Categories = await _menuService.GetPublicMenuAsync();
    }
}
