using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin.Categories;

[ValidateAntiForgeryToken]
public class EditModel : PageModel
{
    private readonly RestaurantDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public EditModel(RestaurantDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public MenuCategory Category { get; set; } = default!;

    [BindProperty]
    public IFormFile? ImageFile { get; set; }

    [BindProperty]
    public bool RemoveImage { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        Category = category;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Category.Id);
        if (categoryToUpdate == null)
        {
            return NotFound();
        }

        // Aggiorna le proprietà base
        categoryToUpdate.Name = Category.Name;
        categoryToUpdate.Description = Category.Description;
        categoryToUpdate.SortOrder = Category.SortOrder;
        categoryToUpdate.IsActive = Category.IsActive;

        // 1. Gestione rimozione immagine esistente
        if (RemoveImage && !string.IsNullOrEmpty(categoryToUpdate.ImageUrl))
        {
            DeleteImageFile(categoryToUpdate.ImageUrl);
            categoryToUpdate.ImageUrl = null;
        }

        // 2. Gestione upload nuova immagine
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ImageFile", "Formato non valido. Usa JPG, PNG o WEBP.");
                return Page();
            }

            // Se esisteva un'immagine precedente, cancella il file fisico
            if (!string.IsNullOrEmpty(categoryToUpdate.ImageUrl))
            {
                DeleteImageFile(categoryToUpdate.ImageUrl);
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "categories");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            categoryToUpdate.ImageUrl = $"/images/categories/{uniqueFileName}";
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    private void DeleteImageFile(string relativePath)
    {
        var fullPath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }
    }
}