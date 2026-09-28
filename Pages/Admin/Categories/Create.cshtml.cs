using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Categories;

[ValidateAntiForgeryToken]
public class CreateModel : PageModel
{
    private readonly RestaurantDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public CreateModel(RestaurantDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public MenuCategory Category { get; set; } = new();

    // Proprietà per gestire il caricamento del file dal form HTML
    [BindProperty]
    public IFormFile? ImageFile { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var maxSort = await _context.Categories
            .Select(c => (int?)c.SortOrder)
            .MaxAsync() ?? 0;

        Category.SortOrder = maxSort + 1;
        Category.IsActive = true;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Gestione dell'upload dell'immagine se fornita
        if (ImageFile != null && ImageFile.Length > 0)
        {
            // Validazione estensione
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ImageFile", "Formato immagine non valido. Usa JPG, PNG o WEBP.");
                return Page();
            }

            // Crea la cartella dest se non esiste
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "categories");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Nome file univoco per evitare sovrascritture
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Salva il file sul disco
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            // Salva il percorso relativo nel database
            Category.ImageUrl = $"/images/categories/{uniqueFileName}";
        }

        _context.Categories.Add(Category);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}