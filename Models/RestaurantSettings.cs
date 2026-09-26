namespace CrottoPlinius.Models;

public class RestaurantSettings
{
    public string Name { get; set; } = "Crotto Plinius";
    public string Tagline { get; set; } = "Cucina Tipica Lariana & Cantina Naturale";
    public string Description { get; set; } = "Autentica tradizione gastronomica del Lago di Como in un ambiente storico scavato nella roccia.";
    public string Address { get; set; } = "Via Regina 42, 22020 Torno (CO), Italia";
    public string Phone { get; set; } = "+39 031 987654";
    public string Email { get; set; } = "info@crottoplinius.it";
    public string OpeningHours { get; set; } = "Mar - Dom: 12:00 - 14:30 | 19:00 - 22:30 (Lunedì Chiuso)";
    public string CurrencySymbol { get; set; } = "€";
    public string GoogleMapsUrl { get; set; } = "https://maps.google.com/?q=Torno+Lake+Como";
}
