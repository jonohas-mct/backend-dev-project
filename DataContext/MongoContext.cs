namespace backend_dev_project.DataContext;

public interface IMongoContext {
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }

    IMongoCollection<Recepe> RecepeCollection { get; }
    IMongoCollection<Ingredient> IngredientCollection { get; }
    IMongoCollection<Utensil> UtensilCollection { get; }
    IMongoCollection<User> UserCollection { get; }

}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Recepe> RecepeCollection
    {
        get
        {
            return _database.GetCollection<Recepe>(_settings.RecepeCollection);
        }
    }

    public IMongoCollection<Ingredient> IngredientCollection
    {
        get
        {
            return _database.GetCollection<Ingredient>(_settings.IngredientCollection);
        }
    }

    public IMongoCollection<Utensil> UtensilCollection
    {
        get
        {
            return _database.GetCollection<Utensil>(_settings.UtensilCollection);
        }
    }

    public IMongoCollection<User> UserCollection
    {
        get
        {
            return _database.GetCollection<User>(_settings.UserCollection);
        }
    }
}