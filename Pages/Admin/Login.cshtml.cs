using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;

namespace CrottoPlinius.Pages.Admin;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly RestaurantDbContext _context;
    private readonly IPasswordHasher<AdminUser> _passwordHasher;

    public LoginModel(RestaurantDbContext context, IPasswordHasher<AdminUser> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ReturnUrl { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "Inserire il nome utente")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Inserire la password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public void OnGet(string? returnUrl = null)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            Response.Redirect("/Admin/Index");
            return;
        }

        ReturnUrl = returnUrl ?? "/Admin/Index";
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= "/Admin/Index";

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _context.AdminUsers
            .FirstOrDefaultAsync(u => u.Username.ToLower() == Input.Username.Trim().ToLower());

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Credenziali non valide.");
            return Page();
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Input.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Credenziali non valide.");
            return Page();
        }

        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, "Administrator")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return LocalRedirect(returnUrl);
    }
}
