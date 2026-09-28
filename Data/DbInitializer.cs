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

            var pizzeriaClassica = new MenuCategory
            {
                Name = "Pizzeria Classica",
                Description = "Pizze e calzoni classici della tradizione",
                SortOrder = 8,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() { Name = "Calzone Liscio", Description = "Pomodoro, Mozzarella", Price = 8.00m, SortOrder = 1, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Calzone Farcito", Description = "Pomodoro, Mozzarella, Prosciutto, Funghi, Olive, Carciofini", Price = 11.00m, SortOrder = 2, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Focaccia Liscia", Description = "", Price = 6.00m, SortOrder = 3, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Focaccia con Crudo", Description = "Sfoglie di Crudo all'uscita", Price = 9.00m, SortOrder = 4, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Marinara", Description = "Pomodoro, Aglio, Basilico", Price = 6.50m, SortOrder = 5, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Margherita", Description = "Pomodoro, Mozzarella", Price = 7.00m, SortOrder = 6, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza con Crudo", Description = "Pomodoro, Mozzarella, Crudo", Price = 10.00m, SortOrder = 7, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza alla Diavola", Description = "Pomodoro, Mozzarella, Salame Piccante, Peperoncino", Price = 10.00m, SortOrder = 8, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza con Prosciutto", Description = "Pomodoro, Mozzarella, Prosciutto Cotto", Price = 10.00m, SortOrder = 9, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza con Salame", Description = "Pomodoro, Mozzarella, Salame Dolce", Price = 10.00m, SortOrder = 10, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza con Speck", Description = "Pomodoro, Mozzarella, Speck all'uscita", Price = 10.00m, SortOrder = 11, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Bresaola e Grana", Description = "Pomodoro, Mozzarella, Bresaola, Grana a scaglie", Price = 11.00m, SortOrder = 12, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Capricciosa", Description = "Pomodoro, Mozzarella, Prosciutto, Funghi, Olive, Carciofini", Price = 10.00m, SortOrder = 13, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Carpaccio e Grana", Description = "Pomodoro, Mozzarella, Carpaccio di Magatello, Grana a scaglie", Price = 10.00m, SortOrder = 14, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Napoletana", Description = "Pomodoro, Mozzarella, Acciughe", Price = 9.50m, SortOrder = 15, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Prosciutto e Funghi", Description = "Pomodoro, Mozzarella, Prosciutto, Funghi Champignon", Price = 10.00m, SortOrder = 16, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Pugliese", Description = "Pomodoro, Mozzarella, Cipolle", Price = 8.50m, SortOrder = 17, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Quattro Formaggi", Description = "Pomodoro, Mozzarella, Gorgonzola, Emmenthal, Taleggio, Brie", Price = 11.00m, SortOrder = 18, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Quattro Stagioni", Description = "Pomodoro, Mozzarella, Prosciutto, Carciofini, Funghetti, Olive", Price = 11.00m, SortOrder = 19, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Frutti di Mare", Description = "Pomodoro, Mozzarella, Frutti di Mare", Price = 12.00m, SortOrder = 20, IsAvailable = true, IsFeatured = false }
                }
            };

            var pizzeriaBenessere = new MenuCategory
            {
                Name = "Pizzeria Benessere",
                Description = "Pizze bianche, leggere e con ingredienti selezionati",
                SortOrder = 9,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() { Name = "Pizza Montebianco", Description = "Mozzarella, Grana, Panna", Price = 9.00m, SortOrder = 21, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Colomba", Description = "Mozzarella, Ananas, Brie", Price = 9.50m, SortOrder = 22, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Selene", Description = "Mozzarella, Insalata, Carote, Zucchine, Pomodorini, Olive", Price = 10.00m, SortOrder = 23, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Favorita", Description = "Mozzarella, Salmone, Panna", Price = 10.00m, SortOrder = 24, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Anthea", Description = "Mozzarella, Gamberetti, Salsa Aurora", Price = 10.00m, SortOrder = 25, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Tonno e Cipolle", Description = "Mozzarella, Tonno, Cipolle", Price = 10.00m, SortOrder = 26, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Dafne", Description = "Mozzarella, Carpaccio di Spada, Rucola, Arancia", Price = 11.00m, SortOrder = 27, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Made in Italy", Description = "Pomodoro, Mozzarella, Pomodorini freschi, Rucola", Price = 10.00m, SortOrder = 28, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza e Porcini", Description = "Pomodoro, Mozzarella, Porcini trifolati", Price = 10.00m, SortOrder = 29, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Vegetariana", Description = "Pomodoro, Mozzarella, Melanzane, Peperoni grigliati, Zucchine affettate", Price = 10.00m, SortOrder = 30, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Zara", Description = "Pomodoro, Mozzarella, Scamorza, Melanzane grigliate, Pomodorini", Price = 10.00m, SortOrder = 31, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Zola e Mele", Description = "Pomodoro, Mozzarella, Gorgonzola, Mele", Price = 10.00m, SortOrder = 32, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Valbrembana", Description = "Pomodoro, Mozzarella, Taleggio, Pere", Price = 10.00m, SortOrder = 33, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza e Gamberetti", Description = "Pomodoro, Mozzarella, Gamberetti", Price = 11.00m, SortOrder = 34, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Mare e Monti", Description = "Pomodoro, Mozzarella, Funghi Porcini, Frutti di Mare", Price = 11.00m, SortOrder = 35, IsAvailable = true, IsFeatured = false }
                }
            };

            var pizzeriaOriginali = new MenuCategory
            {
                Name = "Pizzeria Originali",
                Description = "Abbinamenti sfiziosi e ricette originali della casa",
                SortOrder = 10,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() { Name = "Pizza Lele e Sam", Description = "Mozzarella, Patate bollite, Scamorza Affumicata, Speck, Rosmarino", Price = 11.00m, SortOrder = 36, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Nino", Description = "Mozzarella, Caprino, Radicchio, Salame Piccante all'uscita", Price = 10.00m, SortOrder = 37, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Magda", Description = "Mozzarella, Asparagi, Speck", Price = 11.00m, SortOrder = 38, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Silvana", Description = "Mozzarella, Zucchine grigliate, Pomodoro fresco, Prosciutto Cotto all'uscita", Price = 11.00m, SortOrder = 39, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza all'Inglese", Description = "Pomodoro, Mozzarella, Uovo, Pancetta", Price = 10.00m, SortOrder = 40, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Astrid", Description = "Pomodoro, Mozzarella, Speck, Gorgonzola", Price = 10.00m, SortOrder = 41, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Augusta", Description = "Pomodoro, Mozzarella, Porcini, Lardo, Grana", Price = 12.00m, SortOrder = 42, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Cherubino", Description = "Pomodoro, Mozzarella, Brie, Prosciutto Cotto", Price = 10.00m, SortOrder = 43, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Contadina", Description = "Pomodoro, Mozzarella, Speck, Fagioli, Salame Piccante", Price = 10.00m, SortOrder = 44, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Delizia", Description = "Pomodoro, Mozzarella, Zucchine grigliate, Crudo", Price = 11.00m, SortOrder = 45, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Demetra", Description = "Pomodoro, Mozzarella, Carpaccio di Carne, Sedano, Grana, Olio Tartufato", Price = 12.00m, SortOrder = 46, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Desiderio", Description = "Pomodoro, Mozzarella, Salumi assortiti", Price = 11.00m, SortOrder = 47, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Ethel", Description = "Pomodoro, Mozzarella, Prosciutto Cotto, Ananas", Price = 10.00m, SortOrder = 48, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Maurizio", Description = "Pomodoro, Mozzarella, Gorgonzola, Peperoni grigliati, Peperoncino Piccante", Price = 10.00m, SortOrder = 49, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Tenerife", Description = "Pomodoro, Mozzarella, Patatine Fritte", Price = 11.00m, SortOrder = 50, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Valdostana", Description = "Pomodoro, Mozzarella, Quattro Formaggi, Speck", Price = 11.00m, SortOrder = 51, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Cry 3 P", Description = "Pomodoro, Mozzarella, Prosciutto Cotto, Pesto, Pinoli", Price = 10.00m, SortOrder = 52, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Calzone 30 Anni di Crotto", Description = "Pomodoro, Mozzarella, Zola, Cipolle rosolate, Salame Piccante", Price = 11.00m, SortOrder = 53, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Jordan", Description = "Pomodoro, Mozzarella, Porcini, Speck, Brie", Price = 11.00m, SortOrder = 54, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Würstel", Description = "Pomodoro, Mozzarella, Würstel", Price = 9.50m, SortOrder = 55, IsAvailable = true, IsFeatured = false },
                    new() { Name = "Pizza Würstel e Fritte", Description = "Pomodoro, Mozzarella, Würstel, Patatine Fritte", Price = 12.00m, SortOrder = 56, IsAvailable = true, IsFeatured = false }
                }
            };

            var pizzeriaMensili = new MenuCategory
            {
                Name = "Pizzeria Mensili",
                Description = "Specialità stagionali e proposte del mese",
                SortOrder = 11,
                IsActive = true,
                Dishes = new List<Dish>
                {
                    new() { Name = "Cilento d'Estate", Description = "Pomodoro, Mozzarella, Salmone, Cipolline Borettane, Sesamo", Price = 12.00m, SortOrder = 57, IsAvailable = true, IsFeatured = true },
                    new() { Name = "Giostra del Saracino", Description = "Pomodoro, Pesto di Zucchine, Ciliege di Mozzarella, Pomodori Secchi, Ricotta Stagionata", Price = 13.00m, SortOrder = 58, IsAvailable = true, IsFeatured = true },
                    new() { Name = "Notte di San Lorenzo", Description = "Mozzarella, Porcini, Taleggio, Pancetta Affumicata", Price = 12.50m, SortOrder = 59, IsAvailable = true, IsFeatured = true },
                    new() { Name = "La Volpe e l'Uva", Description = "Mozzarella, Uva, Noci, Grana a Scaglie, Rosmarino", Price = 11.50m, SortOrder = 60, IsAvailable = true, IsFeatured = true },
                    new() { Name = "29 Settembre", Description = "Mozzarella, Pomodoro, Fichi Freschi, Crudo, Zola Piccante", Price = 13.00m, SortOrder = 61, IsAvailable = true, IsFeatured = true }
                }
            };

            context.Categories.AddRange(
                antipastiClassici,
                primiClassici,
                secondiClassici,
                formaggi,
                insalatone,
                contorni,
                menuEstate,
                pizzeriaClassica,
                pizzeriaBenessere,
                pizzeriaOriginali,
                pizzeriaMensili
            );
            context.SaveChanges();
        }
    }
}