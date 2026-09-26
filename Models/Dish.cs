using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrottoPlinius.Models;

public class Dish
{
    public int Id { get; set; }

    [Display(Name = "Categoria")]
    public int MenuCategoryId { get; set; }

    [Required(ErrorMessage = "Il nome del piatto è obbligatorio")]
    [StringLength(150, ErrorMessage = "Il nome non può superare 150 caratteri")]
    [Display(Name = "Nome Piatto")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descrizione non può superare 500 caratteri")]
    [Display(Name = "Descrizione / Ingredienti")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Il prezzo è obbligatorio")]
    [Range(0.01, 999.99, ErrorMessage = "Inserire un prezzo valido (es. 12.50)")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Prezzo (€)")]
    public decimal Price { get; set; }

    [Display(Name = "Ordine")]
    public int SortOrder { get; set; } = 0;

    [Display(Name = "Disponibile oggi")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "Piatto in evidenza")]
    public bool IsFeatured { get; set; } = false;

    [StringLength(200)]
    [Display(Name = "Allergeni (es. Glutine, Lattosio)")]
    public string? Allergens { get; set; }

    [StringLength(500)]
    [Display(Name = "URL Immagine (Opzionale)")]
    public string? ImageUrl { get; set; }

    // Navigation
    public MenuCategory? Category { get; set; }
}
