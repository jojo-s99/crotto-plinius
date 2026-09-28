using System.ComponentModel.DataAnnotations;
using CrottoPlinius.Models;
using CrottoPlinius.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using CrottoPlinius.Models.DTO;


namespace CrottoPlinius.Pages.Admin.Sales;
[Authorize]
public class IndexModel : PageModel
{
    private readonly ISalesService _salesService;
    private readonly IMenuService _menuService;

    public IndexModel(ISalesService salesService, IMenuService menuService)
    {
        _salesService = salesService;
        _menuService = menuService;
    }

    [BindProperty(SupportsGet = true)]
    public int? Year { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? Month { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime SelectedDate { get; set; } = DateTime.Today;

    public List<SalesDayOverviewDto> MonthlyOverview { get; set; } = new();
    public SalesDayDetailDto SelectedDayDetail { get; set; } = new();
    public List<SelectListItem> AvailableDishes { get; set; } = new();

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Seleziona un piatto dal menu")]
        public int DishId { get; set; }

        [Required(ErrorMessage = "Inserisci una quantità")]
        [Range(1, 9999, ErrorMessage = "La quantità deve essere un numero intero positivo")]
        public int Quantity { get; set; } = 1;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var now = DateTime.Today;
        Year ??= SelectedDate != default ? SelectedDate.Year : now.Year;
        Month ??= SelectedDate != default ? SelectedDate.Month : now.Month;

        if (SelectedDate == default)
        {
            SelectedDate = now;
        }

        await LoadDataAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostUpsertSalesItemAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDataAsync();
            return Page();
        }

        await _salesService.UpsertSalesItemAsync(SelectedDate, Input.DishId, Input.Quantity);
        
        TempData["SuccessMessage"] = "Piatto e quantità salvati con successo.";
        return RedirectToPage(new { Year = SelectedDate.Year, Month = SelectedDate.Month, SelectedDate = SelectedDate.ToString("yyyy-MM-dd") });
    }

    public async Task<IActionResult> OnPostRemoveItemAsync(int itemId)
    {
        await _salesService.RemoveSalesItemAsync(itemId);

        TempData["SuccessMessage"] = "Piatto rimosso dalla giornata.";
        return RedirectToPage(new { Year = SelectedDate.Year, Month = SelectedDate.Month, SelectedDate = SelectedDate.ToString("yyyy-MM-dd") });
    }

    private async Task LoadDataAsync()
    {
        Year ??= SelectedDate.Year;
        Month ??= SelectedDate.Month;

        MonthlyOverview = await _salesService.GetMonthlyOverviewAsync(Year.Value, Month.Value);
        SelectedDayDetail = await _salesService.GetSalesDayDetailAsync(SelectedDate);

        var categories = await _menuService.GetAllCategoriesWithDishesAsync();
        
        AvailableDishes = categories
            .SelectMany(c => c.Dishes, (category, dish) => new { category, dish })
            .Select(x => new SelectListItem
            {
                Value = x.dish.Id.ToString(),
                Text = $"{x.category.Name} — {x.dish.Name}"
            })
            .ToList();
    }
}