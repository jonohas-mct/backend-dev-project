namespace backend_dev_project.Repositories;

public interface IUserRepository {

    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
    Task<User> AddUser(User user);
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

}