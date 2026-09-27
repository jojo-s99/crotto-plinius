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
            var antipastiClassici = new MenuCategory
            {
                Name = "Antipasti Classici",
                Description = "Selezione di antipasti di terra e di mare",
                SortOrder = 1,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Bruschette Miste",
                        Description = "Pane bruschettato con Creme Assortite e Contorni vari",
                        Price = 9.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine"
                    },
                    new() {
                        Name = "Antipasto all'Italiana",
                        Description = "Salumi assortiti",
                        Price = 13.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    },
                    new() {
                        Name = "Cozze in Salsa",
                        Description = "Mitili saltati in padella con Pomodoro e Peperoncino",
                        Price = 15.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Molluschi"
                    },
                    new() {
                        Name = "Cozze alla Marinara",
                        Description = "Mitili saltati in padella con Prezzemolo e Vino Bianco",
                        Price = 15.00m,
                        SortOrder = 4,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Molluschi, Solfiti"
                    },
                    new() {
                        Name = "Insalata di Mare",
                        Description = "Piovra, Gamberetti, Polpa di Granchio, Cozze, Vongole, Calamari (Prodotto Surgelato)",
                        Price = 16.00m,
                        SortOrder = 5,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Molluschi, Crostacei"
                    },
                    new() {
                        Name = "Antipasto Misto Mare (Assaggi)",
                        Description = "Salmone e Spada affumicati, Insalata di Mare, Cocktail di Gamberetti, Alici marinate (Prodotto Surgelato)",
                        Price = 16.00m,
                        SortOrder = 6,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce, Molluschi, Crostacei"
                    },
                    new() {
                        Name = "Pocker d'Assi",
                        Description = "Frittini misti con Gamberetti in Pastafilo, Alici Marinate, Verdure Pastellate e Chele di Granchio Panate (Prodotto Surgelato)",
                        Price = 15.00m,
                        SortOrder = 7,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Crostacei, Pesce"
                    }
                }
            };

            var primiClassici = new MenuCategory
            {
                Name = "Primi Piatti Classici",
                Description = "Primi piatti di pasta tradizionali di terra e mare",
                SortOrder = 2,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Tortellini Panna Speck e Zafferano",
                        Description = "Fagottini di Pasta ripieni con Carne, rosolati con Speck e mantecati con Panna e Zafferano",
                        Price = 13.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Lattosio"
                    },
                    new() {
                        Name = "Pennette al Sapore di Granchio",
                        Description = "Pasta di Grano Duro con Salsa di Pomodoro e Surimi flambato al Brandy (Prodotto Surgelato)",
                        Price = 14.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Crostacei"
                    },
                    new() {
                        Name = "Tagliolini agli Agrumi",
                        Description = "Listarelle di Pasta di Grano con Arancia e Limone alla Julienne e punta di Panna",
                        Price = 14.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Lattosio"
                    },
                    new() {
                        Name = "Spaghetti allo Scoglio",
                        Description = "Stringhe di Pasta al Grano Duro con Salsa di Pomodoro, Cozze, Piovra, Seppie, Gamberi, Surimi, Anelli di Totano e Latte e Fondo Crostacei (Prodotto Surgelato)",
                        Price = 17.00m,
                        SortOrder = 4,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Molluschi, Crostacei, Lattosio"
                    },
                    new() {
                        Name = "Spaghetti alle Vongole",
                        Description = "Stringhe sottili di Pasta di Grano Duro saltate in padella con Vongole sfumate al Vino Bianco e Guazzetto di Pesce (Prodotto Surgelato)",
                        Price = 17.00m,
                        SortOrder = 5,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Molluschi, Pesce, Solfiti"
                    },
                    new() {
                        Name = "Spaghetti con Porri e Pistacchi",
                        Description = "Stringhe di Farro con ragù di Porri e trito di Pistacchi con Salsa brunita",
                        Price = 15.00m,
                        SortOrder = 6,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Frutta a guscio"
                    }
                }
            };

            var secondiClassici = new MenuCategory
            {
                Name = "Secondi Piatti Classici",
                Description = "Specialità di carne e pesce alla griglia e fritti",
                SortOrder = 3,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Filetto di Branzino agli Agrumi",
                        Description = "Branzino filettato e passato in padella con Succo e Buccia di Arancia, Limone, Fondo Crostacei (Prodotto Surgelato)",
                        Price = 18.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Pesce, Crostacei"
                    },
                    new() {
                        Name = "Filetto di Manzo ai Ferri",
                        Description = "Taglio di pregio di Manzo alla griglia",
                        Price = 20.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = null
                    },
                    new() {
                        Name = "Scaloppine ai Porcini",
                        Description = "Fettine di Arista con Funghi Porcini e Panna accompagnate da Salsa brunita (Prodotto Surgelato)",
                        Price = 18.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Lattosio"
                    },
                    new() {
                        Name = "Tagliata di Manzo alle Erbe Fini",
                        Description = "Listarelle di Manzo adagiate su Rucola e Scaglie di Grana dorate con Burro fuso ed Erbette",
                        Price = 20.00m,
                        SortOrder = 4,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Lattosio"
                    },
                    new() {
                        Name = "Grigliata di Carne (min. x 2)",
                        Description = "Arista, Salamella, Fesa di Tacchino o Petto di Pollo, Manzo, Würstel, Pancetta, Scottadito di Agnello (Prezzo a persona)",
                        Price = 18.00m,
                        SortOrder = 5,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = null
                    },
                    new() {
                        Name = "Grigliata Mista di Pesce",
                        Description = "Salmone, Spada, Seppia, Gamberoni, Filetto di Orata o Branzino secondo disponibilità (Prodotto Surgelato)",
                        Price = 22.00m,
                        SortOrder = 6,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce, Molluschi, Crostacei"
                    },
                    new() {
                        Name = "Gamberoni alla Pietra",
                        Description = "Gamberi di Mare grigliati (Prodotto Surgelato)",
                        Price = 20.00m,
                        SortOrder = 7,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Crostacei"
                    },
                    new() {
                        Name = "Anelli di Calamari",
                        Description = "Rondelle di Totano infarinate e fritte (Prodotto Surgelato)",
                        Price = 16.00m,
                        SortOrder = 8,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine, Molluschi"
                    },
                    new() {
                        Name = "Fritto Misto di Pesce",
                        Description = "Totani, Gamberetti, Ciuffi di Piovra, Acquadelle pastellate e fritte (Prodotto Surgelato)",
                        Price = 18.00m,
                        SortOrder = 9,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Pesce, Molluschi, Crostacei"
                    }
                }
            };

            var formaggi = new MenuCategory
            {
                Name = "Formaggi",
                Description = "Selezione di formaggi locali e stagionati",
                SortOrder = 4,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Tagliere di Formaggi",
                        Description = "Tris di Formaggi a scelta",
                        Price = 15.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Lattosio"
                    },
                    new() {
                        Name = "Formaggio Singolo",
                        Description = "Grana, Taleggio, Gorgonzola, Pecorino...",
                        Price = 5.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Lattosio"
                    }
                }
            };

            var insalatone = new MenuCategory
            {
                Name = "Insalatone",
                Description = "Ricche insalate di terra e di mare",
                SortOrder = 5,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Insalata Tirolese",
                        Description = "Noci, Mele, Speck",
                        Price = 10.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Frutta a guscio"
                    },
                    new() {
                        Name = "Insalata del Pescatore",
                        Description = "Insalata con Frutti di Mare (Prodotto Surgelato)",
                        Price = 13.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Molluschi, Crostacei"
                    },
                    new() {
                        Name = "Insalata alla Mugnaia",
                        Description = "Verdure grigliate, Mozzarella",
                        Price = 10.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Lattosio"
                    },
                    new() {
                        Name = "Insalata Norvegese",
                        Description = "Salmone, Mozzarella",
                        Price = 10.00m,
                        SortOrder = 4,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Pesce, Lattosio"
                    },
                    new() {
                        Name = "Insalata Raffinata",
                        Description = "Speck, Brie",
                        Price = 10.00m,
                        SortOrder = 5,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Lattosio"
                    },
                    new() {
                        Name = "Insalata Vigorosa",
                        Description = "Tonno, Cipolline in agrodolce",
                        Price = 10.00m,
                        SortOrder = 6,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Pesce"
                    },
                    new() {
                        Name = "Insalata del Lago",
                        Description = "Gamberetti, Pinoli, Zucchine grigliate (Prodotto Surgelato)",
                        Price = 11.00m,
                        SortOrder = 7,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Crostacei, Frutta a guscio"
                    },
                    new() {
                        Name = "Insalata Piccolo Gnomo",
                        Description = "Champignon, Bresaola, Caprino",
                        Price = 10.00m,
                        SortOrder = 8,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Lattosio"
                    }
                }
            };

            var contorni = new MenuCategory
            {
                Name = "Contorni",
                Description = "Contorni di verdure, patate e sfiziosità",
                SortOrder = 6,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Insalata Mista",
                        Description = "Verdura cruda a foglia verde, Pomodori, Finocchi, Peperoni, Carote",
                        Price = 5.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    },
                    new() {
                        Name = "Verdure Cotte",
                        Description = "Selezione di Ortaggi bolliti che variano a seconda della disponibilità settimanale",
                        Price = 5.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    },
                    new() {
                        Name = "Patatine Fritte",
                        Description = "Patatine fritte (Prodotto Surgelato)",
                        Price = 5.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    },
                    new() {
                        Name = "Crocchette di Patate",
                        Description = "Cilindri di Patate panati e fritti (Prodotto Surgelato)",
                        Price = 6.00m,
                        SortOrder = 4,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine"
                    },
                    new() {
                        Name = "Olive all'Ascolana",
                        Description = "Olive verdi dalla polpa carnosa ripiene con Carne trita e Verdure, pastellate e fritte (Prodotto Surgelato)",
                        Price = 6.00m,
                        SortOrder = 5,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine"
                    },
                    new() {
                        Name = "Verdure Grigliate",
                        Description = "Melanzane, Peperoni, Zucchine",
                        Price = 6.00m,
                        SortOrder = 6,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = null
                    },
                    new() {
                        Name = "Verdure Pastellate",
                        Description = "Verdure miste passate in Pastella e Fritte",
                        Price = 6.00m,
                        SortOrder = 7,
                        IsAvailable = true,
                        IsFeatured = false,
                        Allergens = "Glutine"
                    }
                }
            };

            var menuEstate = new MenuCategory
            {
                Name = "Menu Estate",
                Description = "Proposte speciali e piatti estivi d'autore",
                SortOrder = 7,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() {
                        Name = "Pocker d'Assi (Estate)",
                        Description = "Frittini Misti con Gamberetti e Pasta Filo, Alici Marinate, Verdure Pastellate e Chele di Granchio Panate",
                        Price = 15.00m,
                        SortOrder = 1,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Crostacei, Pesce"
                    },
                    new() {
                        Name = "Voci dalle Cime",
                        Description = "Salumi Nostrani con Sciatt della Valtellina su Letto di Valeriana e Fondente di Casera",
                        Price = 15.00m,
                        SortOrder = 2,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Lattosio"
                    },
                    new() {
                        Name = "Il Canto delle Sirene",
                        Description = "Insalatona di Avocado, Feta, Pomodorini Confit, Tartare di Branzino, Crostini di Pane e Cipolle Caramellate",
                        Price = 14.00m,
                        SortOrder = 3,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce, Lattosio, Glutine"
                    },
                    new() {
                        Name = "Il Bacio del Duca",
                        Description = "Scialatielli con Pomodorini Confit, Burrata e Acciughe",
                        Price = 15.00m,
                        SortOrder = 4,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Lattosio, Pesce"
                    },
                    new() {
                        Name = "Nemesi",
                        Description = "Crespelle con Pesto di Pistacchi e Speck",
                        Price = 16.00m,
                        SortOrder = 5,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine, Lattosio, Frutta a guscio, Uova"
                    },
                    new() {
                        Name = "Sole a Catinelle",
                        Description = "Bavette Limone e Pepe con Porri, Guanciale e Cipolle Caramellate",
                        Price = 15.00m,
                        SortOrder = 6,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Glutine"
                    },
                    new() {
                        Name = "Stelle Cadenti",
                        Description = "Tagliata di Tonno, Ananas e Avocado alla Piastra",
                        Price = 25.00m,
                        SortOrder = 7,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce"
                    },
                    new() {
                        Name = "Una Notte da Leoni",
                        Description = "Costata di Manzo alla Griglia con Patate Saltate",
                        Price = 27.00m,
                        SortOrder = 8,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = null
                    },
                    new() {
                        Name = "Travolti da un Insolito Destino nell'Azzurro Mare d'Agosto",
                        Description = "Filetto di Persico Dorato",
                        Price = 22.00m,
                        SortOrder = 9,
                        IsAvailable = true,
                        IsFeatured = true,
                        Allergens = "Pesce, Glutine"
                    }
                }
            };

            context.Categories.AddRange(
                antipastiClassici,
                primiClassici,
                secondiClassici,
                formaggi,
                insalatone,
                contorni,
                menuEstate
            );
            context.SaveChanges();
        }
    }
}