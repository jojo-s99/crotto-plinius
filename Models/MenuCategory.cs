using System.ComponentModel.DataAnnotations;

namespace CrottoPlinius.Models;

public class MenuCategory
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Il nome della categoria è obbligatorio")]
    [StringLength(100, ErrorMessage = "Il nome non può superare 100 caratteri")]
    [Display(Name = "Nome Categoria")]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    [Display(Name = "Descrizione")]
    public string? Description { get; set; }

    [Display(Name = "Ordine di visualizzazione")]
    public int SortOrder { get; set; } = 0;

    [Display(Name = "Attiva nel Menu")]
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
