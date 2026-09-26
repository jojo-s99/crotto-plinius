using System.ComponentModel.DataAnnotations;

namespace CrottoPlinius.Models;

public class Menu
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "Menu Principale";

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidUntil { get; set; }

    public bool IsPublished { get; set; } = true;
}
