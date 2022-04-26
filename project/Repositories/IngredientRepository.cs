namespace backend_dev_project.Repositories;

public interface IIngredientRepository {
    Task<List<Ingredient>> GetIngredients();
}

public class IngredientRepository : IIngredientRepository {
    private readonly IMongoContext _context;

    public IngredientRepository (IMongoContext context) {

        _context = context;

    }

    public async Task<List<Ingredient>> GetIngredients () {
        var alles = await _context.IngredientCollection.Find(_ => true).ToListAsync();
        return alles;
    }

}