using Microsoft.AspNetCore.Identity;
using CrottoPlinius.Models;

namespace CrottoPlinius.Data;

public static class DbInitializer
{
    public static void Initialize(RestaurantDbContext context, IPasswordHasher<AdminUser> passwordHasher)
    {
        context.Database.EnsureCreated();

        // Idempotent Admin User Seeding
        if (!context.AdminUsers.Any())
        {
            var admin = new AdminUser
            {
                Username = "admin"
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "CrottoPlinius2026!");
            context.AdminUsers.Add(admin);
            context.SaveChanges();
        }

        // Idempotent Menu Seeding
        if (!context.Categories.Any())
        {
            var antipasti = new MenuCategory
            {
                Name = "Antipasti",
                Description = "Sapori autentici del Lago di Como e salumi della Valtellina",
                SortOrder = 1,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Tagliere del Crotto",
                        Description = "Selezione di bresaola della Valtellina IGP, salame nostrano, formaggi d'alpeggio e miele di castagno",
                        Price = 16.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Lattosio"
                    },
                    new() {
                        Name = "Missoltini del Lario con Polenta",
                        Description = "Agoni essiccati al sole, grigliati e serviti con polenta taragna fumante e olio extravergine",
                        Price = 14.50m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce"
                    },
                    new() {
                        Name = "Sciatt Croccanti su Letto di Cicoria",
                        Description = "Frittelle di grano saraceno ripiene di formaggio Casera DOP fuso",
                        Price = 12.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Lattosio"
                    }
                }
            };

            var primi = new MenuCategory
            {
                Name = "Primi Piatti",
                Description = "Pasta fresca fatta in casa e risotti della tradizione",
                SortOrder = 2,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Risotto al Pesce Persico",
                        Description = "Carnaroli mantecato al burro e salvia con filetti di pesce persico dorati",
                        Price = 18.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce, Lattosio"
                    },
                    new() {
                        Name = "Pizzoccheri alla Valtellinese",
                        Description = "Tagliatelle di grano saraceno fatte a mano, verze, patate, formaggio Casera e burro fuso aromatizzato all'aglio",
                        Price = 14.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Lattosio"
                    },
                    new() {
                        Name = "Gnocchetti di Castagne al Ragù di Capriolo",
                        Description = "Gnocchetti artigianali di farina di castagne con stufato di capriolo al vino rosso di Valtellina",
                        Price = 15.50m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Sedano"
                    }
                }
            };

            var secondi = new MenuCategory
            {
                Name = "Secondi Piatti",
                Description = "Specialità di lago e carni cotte a fuoco lento",
                SortOrder = 3,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Lavarello alla Griglia con Salmoriglio",
                        Description = "Coregone pescato nel lago, grigliato alle erbe lariane e limone",
                        Price = 17.50m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce"
                    },
                    new() {
                        Name = "Brasato di Manzo al Sassella con Polenta",
                        Description = "Guancetta di manzo brasata per 6 ore nel vino rosso Sassella servita con polenta rustica macinata a pietra",
                        Price = 19.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Sedano, Solfiti"
                    },
                    new() {
                        Name = "Costine di Maiale alla Crotto",
                        Description = "Cotte al forno a legna con timo selvatico, ginepro e rosmarino",
                        Price = 15.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    }
                }
            };

            var dolci = new MenuCategory
            {
                Name = "Dolci Artigianali",
                Description = "Preparati ogni mattina nella nostra pasticceria",
                SortOrder = 4,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Miascia del Lario",
                        Description = "Dolce tradizionale comasco con pane raffermo, mele, uvetta, fichi secchi e cannella",
                        Price = 6.50m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Frutta a guscio, Lattosio"
                    },
                    new() {
                        Name = "Tiramisù al Mascarpone d'Alpeggio",
                        Description = "Con savoiardi caserecci e caffè moka selezione 100% arabica",
                        Price = 6.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Uova, Lattosio, Glutine"
                    }
                }
            };

            var bevande = new MenuCategory
            {
                Name = "Cantina & Bevande",
                Description = "Selezione enologica del territorio e bibite",
                SortOrder = 5,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Sassella Valtellina Superiore DOCG (Bottiglia)",
                        Description = "100% Nebbiolo (Chiavennasca), elegante, minerale, profumo di frutti rossi",
                        Price = 28.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Solfiti"
                    },
                    new() {
                        Name = "Vino della Casa Rosso / Bianco (1/2 Litro)",
                        Description = "Vino sfuso da tavola dei vigneti terrazzati lariani",
                        Price = 7.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Solfiti"
                    },
                    new() {
                        Name = "Acqua Minerale Naturale / Frizzante (0.75L)",
                        Description = "Sorgente Chiarella",
                        Price = 2.50m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    },
                    new() {
                        Name = "Caffè Espresso",
                        Description = "Torrefazione artigianale lombarda",
                        Price = 1.50m,
                        SortOrder = 4,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    }
                }
            };

            context.Categories.AddRange(antipasti, primi, secondi, dolci, bevande);
            context.SaveChanges();
        }
    }
}
