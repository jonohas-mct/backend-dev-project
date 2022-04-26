namespace backend_dev_project.Repositories;

public interface IUserRepository {

    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
    Task<User> AddUser(User user);

    Task<List<Recepe>> AddFavorites(string userId, List<string> favorites);
    Task<List<Recepe>> RemoveFavorites(string userId, string[] favorites);
    Task<List<Recepe>> GetFavorites(string userId);
    
}

public class UserRepository : IUserRepository {
    private readonly IMongoContext _context;

    public UserRepository (IMongoContext context) {

        _context = context;

    }
    public async Task<List<User>> GetUsers () {
        var alles = await _context.UserCollection.Find(_ => true).ToListAsync();
        return alles;
    }
    public async Task<User> GetUser (string username) {
        return await _context.UserCollection.Find<User>(r => r.Username == username).FirstOrDefaultAsync();
    }

    public async Task<User> AddUser (User user) {
        try {
            await _context.UserCollection.InsertOneAsync(user);
            return user;
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Recepe>> GetFavorites(string username) {
        var user  = await _context.UserCollection.Find<User>(u => u.Username == username).FirstOrDefaultAsync();

        if (user == null) {
            return null;
        }
        List<Recepe> favorites = new List<Recepe>();

        foreach (var favorite in user.Favorites)
        {
            var recepe  = await _context.RecepeCollection.Find<Recepe>(r => r.Id == favorite).FirstOrDefaultAsync();
            favorites.Add(recepe);
        }
        

        return favorites; 
    }

    public async Task<List<Recepe>> AddFavorites (string username, List<string> favorites) {
        var user = await _context.UserCollection.Find<User>(u => u.Username == username).FirstOrDefaultAsync();

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
        var filter = Builders<User>.Filter.Eq("Username", user.Username);
        await _context.UserCollection.ReplaceOneAsync(filter, user, new ReplaceOptions() {IsUpsert = true});

        List<Recepe> fav = new List<Recepe>();

        foreach (var favorite in user.Favorites)
        {
            var recepe  = await _context.RecepeCollection.Find<Recepe>(r => r.Id == favorite).FirstOrDefaultAsync();
            fav.Add(recepe);
        }
        return fav;
    }

    public async Task<List<Recepe>> RemoveFavorites (string username, string[] favorites) {
        var user = await _context.UserCollection.Find<User>(u => u.Username == username).FirstOrDefaultAsync();

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

        var filter = Builders<User>.Filter.Eq("Username", user.Username);
        await _context.UserCollection.ReplaceOneAsync(filter, user, new ReplaceOptions() {IsUpsert = true});

        List<Recepe> fav = new List<Recepe>();

        foreach (var favorite in user.Favorites)
        {
            var recepe  = await _context.RecepeCollection.Find<Recepe>(r => r.Id == favorite).FirstOrDefaultAsync();
            fav.Add(recepe);
        }
        return fav;
    }
}





