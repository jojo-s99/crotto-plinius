using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CrottoPlinius.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Pagina statica per l'informativa Privacy e Cookie tecnici.
            // Non richiede elaborazione dati nel backend.
        }
    }
}