using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;
using CrottoPlinius.Models.DTO;

namespace CrottoPlinius.Services;


public interface ISalesService
{
    Task<List<SalesDayOverviewDto>> GetMonthlyOverviewAsync(int year, int month);
    Task<SalesDayDetailDto> GetSalesDayDetailAsync(DateTime date);
    Task UpsertSalesItemAsync(DateTime date, int dishId, int quantity);
    Task RemoveSalesItemAsync(int salesItemId);
    Task DeleteSalesDayAsync(int salesDayId);
}

public class SalesService : ISalesService
{
    private readonly RestaurantDbContext _context;

    public SalesService(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<List<SalesDayOverviewDto>> GetMonthlyOverviewAsync(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var salesDays = await _context.SalesDays
            .AsNoTracking()
            .Where(sd => sd.Date >= startDate && sd.Date <= endDate)
            .Select(sd => new
            {
                sd.Date,
                TotalDishes = sd.Items.Sum(i => i.Quantity)
            })
            .ToListAsync();

        var daysInMonth = DateTime.DaysInMonth(year, month);
        var result = new List<SalesDayOverviewDto>();

        for (int day = 1; day <= daysInMonth; day++)
        {
            var currentDate = new DateTime(year, month, day);
            var existing = salesDays.FirstOrDefault(s => s.Date.Date == currentDate.Date);

            result.Add(new SalesDayOverviewDto
            {
                Date = currentDate,
                HasData = existing != null && existing.TotalDishes > 0,
                TotalDishesSold = existing?.TotalDishes ?? 0
            });
        }

        return result;
    }

    public async Task<SalesDayDetailDto> GetSalesDayDetailAsync(DateTime date)
    {
        var targetDate = date.Date;

        var salesDay = await _context.SalesDays
            .AsNoTracking()
            .Include(sd => sd.Items)
                .ThenInclude(si => si.Dish)
                    .ThenInclude(d => d!.Category)
            .FirstOrDefaultAsync(sd => sd.Date == targetDate);

        if (salesDay == null)
        {
            return new SalesDayDetailDto
            {
                Date = targetDate,
                Items = new List<SalesItemDto>()
            };
        }

        return new SalesDayDetailDto
        {
            Id = salesDay.Id,
            Date = salesDay.Date,
            Notes = salesDay.Notes,
            Items = salesDay.Items.Select(item => new SalesItemDto
            {
                Id = item.Id,
                DishId = item.DishId,
                DishName = item.Dish != null ? item.Dish.Name : "Piatto rimosso",
                CategoryName = item.Dish?.Category?.Name ?? "Senza Categoria",
                Quantity = item.Quantity
            }).ToList()
        };
    }


/// <summary>
    /// Inserisce o aggiorna la quantità venduta di un piatto per una determinata data.
    /// </summary>
    public async Task UpsertSalesItemAsync(DateTime date, int dishId, int quantity)
    {
        var targetDate = date.Date;

        // 1. Recupera il SalesDay o crealo se non esiste ancora
        var salesDay = await _context.SalesDays
            .FirstOrDefaultAsync(sd => sd.Date.Date == targetDate);

        if (salesDay == null)
        {
            salesDay = new SalesDay 
            { 
                Date = targetDate 
            };
            _context.SalesDays.Add(salesDay);
            // Salva subito per generare l'Id di SalesDay indispensabile per la FK di SalesItem
            await _context.SaveChangesAsync();
        }

        // 2. Cerca se l'item per questo piatto esiste già nella giornata
        var existingItem = await _context.SalesItems
            .FirstOrDefaultAsync(si => si.SalesDayId == salesDay.Id && si.DishId == dishId);

        if (existingItem != null)
        {
            // Se la quantità è 0 o minore, lo rimuoviamo
            if (quantity <= 0)
            {
                _context.SalesItems.Remove(existingItem);
            }
            else
            {
                // Altrimenti aggiorniamo la quantità
                existingItem.Quantity = quantity;
            }
        }
        else if (quantity > 0)
        {
            // 3. Se l'item non esiste e la quantità > 0, recupera il prezzo dal piatto
            var dish = await _context.Dishes.FindAsync(dishId);
            if (dish == null)
            {
                throw new KeyNotFoundException($"Piatto con ID {dishId} non trovato.");
            }

            var newItem = new SalesItem
            {
                SalesDayId = salesDay.Id,
                DishId = dishId,
                Quantity = quantity,
                UnitPrice = dish.Price
            };

            _context.SalesItems.Add(newItem);
        }

        await _context.SaveChangesAsync();
    }

   
    public async Task RemoveSalesItemAsync(int salesItemId)
    {
        var item = await _context.SalesItems.FindAsync(salesItemId);
        if (item != null)
        {
            int salesDayId = item.SalesDayId;
            _context.SalesItems.Remove(item);
            await _context.SaveChangesAsync();

            var hasRemainingItems = await _context.SalesItems.AnyAsync(si => si.SalesDayId == salesDayId);
            if (!hasRemainingItems)
            {
                await DeleteSalesDayAsync(salesDayId);
            }
        }
    }

    public async Task DeleteSalesDayAsync(int salesDayId)
    {
        var salesDay = await _context.SalesDays.FindAsync(salesDayId);
        if (salesDay != null)
        {
            _context.SalesDays.Remove(salesDay);
            await _context.SaveChangesAsync();
        }
    }
}