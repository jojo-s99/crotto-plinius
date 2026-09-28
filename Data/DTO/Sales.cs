using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrottoPlinius.Models.DTO;


    public class SalesDayOverviewDto
    {
        public DateTime Date { get; set; }
        public bool HasData { get; set; }
        public int TotalItemsSold { get; set; }
        public int TotalDishesSold { get; set; } // Aggiunto per SalesService e Index.cshtml
        public decimal TotalRevenue { get; set; }
    }

    public class SalesDayDetailDto
    {
        public int? Id { get; set; } // Mappato per SalesService
        public int? SalesDayId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public List<SalesItemDto> Items { get; set; } = new();
        public int TotalQuantity => Items.Sum(i => i.Quantity); // Aggiunto per Index.cshtml
        public decimal TotalRevenue => Items.Sum(i => i.TotalPrice);
    }

    public class SalesItemDto
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
