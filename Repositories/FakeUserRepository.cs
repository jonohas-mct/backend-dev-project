namespace backend_dev_project.Repositories;


public class FakeUserRepository : IUserRepository {

    private static List<User> _users = new List<User>();
    private static List<Recepe> _recepes = new List<Recepe>();

    private readonly FakeRecepeRepository _fakeRecepeRepository;

    public FakeUserRepository () {

        _users.Add(new User() {
            Id = "1",
            Username = "JonahasFake",
            Email = "jonas.h.faber@fa.ke",
            Password = "$2a$11$o/JoOhKOEde3F9FKjZpGqeMAjkj2ALesRx4V6YcDb9DL0DE.551e.",
            Favorites = new List<string>() {
                "1",
                "2"
            }
        });

        _recepes.Add(new Recepe() {
            Id = "1",
            Name = "Pancakes",
            Ingredients = new List<IngredientAmount>() {
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Suggar"
                    },
                    Amount = 100,
                    Unit = "g"
                },
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Eggs"
                    },
                    Amount = 2
                }
            },
            DurationMinutes = 15,
            Steps = new List<string> () {
                "Zeef de bloem met het zout boven een grote beslagkom.",
                "Voeg de eiren en de helft van de melk toe.",
                "Klop met een garde tot een glad beslag.",
                "Schenk de rest van de melk erbij en klop opnieuw glad.",
                "Dek af met vershoudfolie en laat het beslag 30 min. staan.",
                "Verhit een klontje boter in een koekenpan van 20 cm doorsnee en schep er een soeplepel beslag in.",
                "Draai de pan rond zodat de hele bodem bedekt is.",
                "Laat de pannekoek 2 min. op middelhoog vuur bakken tot de bovenkant droog is en de onderkant goudbruin.",
                "Draai de pannenkoek om en bak nog 1 min.",
                "Herhaal met de rest van het beslag"
            },
            Utensils = new List<Utensil>() {
                new Utensil() {Name = "Beslagkom"},
                new Utensil() {Name = "Zeef"},
                new Utensil() {Name = "Garde"},
                new Utensil() {Name = "Vershoudfolie"},
                new Utensil() {Name = "Soeplepel"},
                new Utensil() {Name = "Pan"},
            }
        });

        _recepes.Add(new Recepe() {
            Id = "2",
            Name = "Appelflap",
            Ingredients = new List<IngredientAmount>() {
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Suggar"
                    },
                    Amount = 100,
                    Unit = "g"
                },
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Eggs"
                    },
                    Amount = 2
                },
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Appels"
                    },
                    Amount = 4
                }
            },
            Steps = new List<string> () {
                "Zeef de bloem met het zout boven een grote beslagkom.",
                "Voeg de eiren en de helft van de melk toe.",
                "Klop met een garde tot een glad beslag.",
                "Schenk de rest van de melk erbij en klop opnieuw glad.",
                "Dek af met vershoudfolie en laat het beslag 30 min. staan.",
                "Verhit een klontje boter in een koekenpan van 20 cm doorsnee en schep er een soeplepel beslag in.",
                "Draai de pan rond zodat de hele bodem bedekt is.",
                "Laat de pannekoek 2 min. op middelhoog vuur bakken tot de bovenkant droog is en de onderkant goudbruin.",
                "Draai de pannenkoek om en bak nog 1 min.",
                "Herhaal met de rest van het beslag"
            },
            Utensils = new List<Utensil>() {
                new Utensil() {Name = "Beslagkom"},
                new Utensil() {Name = "Zeef"},
                new Utensil() {Name = "Garde"},
                new Utensil() {Name = "Vershoudfolie"},
                new Utensil() {Name = "Soeplepel"},
                new Utensil() {Name = "Pan"},
            }
        });

    }
    public async Task<List<User>> GetUsers () {
        return _users;
    }
    public async Task<User> GetUser (string username) {
        return _users.Find(r => r.Username == username);
    }

    public async Task<User> AddUser (User user) {
        try {
            _users.Add(user);
            return user;
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Recepe>> GetFavorites(string username) {
        var user = _users.FirstOrDefault(r => r.Username == username);

        if (user == null) {
            return null;
        }

        List<Recepe> favorites = new List<Recepe>();

        foreach (var favorite in user.Favorites)
        {
            
            var recepe = _recepes.FirstOrDefault(r => r.Id == favorite);
            favorites.Add(recepe);
        }

        return favorites; 
    }

    public async Task<List<Recepe>> AddFavorites (string username, List<string> favorites) {
        var user = _users.FirstOrDefault(r => r.Username == username);

        if (user == null) {
            return null;
        }

        for (int i = 0; i < favorites.Count; i++)
        {
            var exists = user.Favorites.Find(f => f == favorites[i]);
            if (exists == null) {
                user.Favorites.Add(favorites[i]);
            }
        }
        List<Recepe> favoritesList = new List<Recepe>();

        foreach (var favorite in user.Favorites)
        {
            
            var recepe = _recepes.FirstOrDefault(r => r.Id == favorite);
            favoritesList.Add(recepe);
        }

        return favoritesList; 
    }

    public async Task<List<Recepe>> RemoveFavorites (string username, string[] favorites) {
        var user = _users.FirstOrDefault(r => r.Username == username);

        if (user == null) {
            return null;
        }

        foreach (var fa in favorites)
        {
            var exists = user.Favorites.Find(f => f == fa);
            if (exists != null) {
                user.Favorites.Remove(fa);
            }
        }
        List<Recepe> favoritesList = new List<Recepe>();

        foreach (var favorite in user.Favorites)
        {
            
            var recepe = _recepes.FirstOrDefault(r => r.Id == favorite);
            favoritesList.Add(recepe);
        }

        return favoritesList; 
    }
}





