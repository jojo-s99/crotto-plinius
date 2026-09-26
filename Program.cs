using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CrottoPlinius.Data;
using CrottoPlinius.Models;
using CrottoPlinius.Services;

var builder = WebApplication.CreateBuilder(args);

// Strongly-typed options
builder.Services.Configure<RestaurantSettings>(
    builder.Configuration.GetSection("RestaurantSettings"));

// Database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? "Data Source=crotto_plinius.db";
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlite(connectionString));

// Security & Password Hasher
builder.Services.AddScoped<IPasswordHasher<AdminUser>, PasswordHasher<AdminUser>>();

// Application Services
builder.Services.AddScoped<IMenuService, MenuService>();

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/Admin/Login";
        options.Cookie.Name = "CrottoPlinius.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.ExpireTimeSpan = TimeSpan.FromHours(12);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// Razor Pages configuration
builder.Services.AddRazorPages(options =>
{
    // Secure /Admin area while keeping login accessible
    options.Conventions.AuthorizeFolder("/Admin");
    options.Conventions.AllowAnonymousToPage("/Admin/Login");
});

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});

var app = builder.Build();

// Ensure DB and seed baseline data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RestaurantDbContext>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher<AdminUser>>();
    DbInitializer.Initialize(context, passwordHasher);
}

// HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
