using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrottoPlinius.Models;


public class SalesDay
{
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string? Notes { get; set; }

    // Relazione 1-a-N con SalesItem
    public ICollection<SalesItem> Items { get; set; } = new List<SalesItem>();
}

public class SalesItem
{
    public int Id { get; set; }

    public int SalesDayId { get; set; }
    public SalesDay SalesDay { get; set; } = null!;

    public int DishId { get; set; }
    public Dish Dish { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
}