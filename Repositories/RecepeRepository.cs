namespace backend_dev_project.Repositories;

public interface IRecepeRepository {
    Task<List<Recepe>> GetRecepes();
    Task<Recepe> GetRecepe(string recepeId);
    Task<Recepe> AddRecepe(Recepe recepe);
    Task<Recepe> UpdateRecepe(Recepe recepe);
    Task DeleteRecepe(string recepeId);
}

public class RecepeRepository : IRecepeRepository {
    private readonly IMongoContext _context;
    private static List<Recepe> _recepes = new List<Recepe>();

    public RecepeRepository (IMongoContext context) {

        _context = context;

    }

    public async Task<List<Recepe>> GetRecepes () {
        var alles = await _context.RecepeCollection.Find(_ => true).ToListAsync();
        return alles;
    }

    public async Task<Recepe> GetRecepe (string recepeId) {
        return await _context.RecepeCollection.Find<Recepe>(r => r.Id == recepeId).FirstOrDefaultAsync();
    }

    public async Task<Recepe> AddRecepe (Recepe recepe) {
        try {
            foreach (IngredientAmount ingredientAmount in recepe.Ingredients)
            {
                var searchIngredient = await _context.IngredientCollection.Find<Ingredient>(r => r.Name == ingredientAmount.Ingredient.Name).FirstOrDefaultAsync();

                if (searchIngredient == null) {
                    await _context.IngredientCollection.InsertOneAsync(ingredientAmount.Ingredient);
                }

                ingredientAmount.Ingredient = await _context.IngredientCollection.Find<Ingredient>(r => r.Name == ingredientAmount.Ingredient.Name).FirstOrDefaultAsync();
            }
            await _context.RecepeCollection.InsertOneAsync(recepe);
            return recepe;
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Recepe> UpdateRecepe( Recepe recepe) {
        try {
            var filter = Builders<Recepe>.Filter.Eq("Id", recepe.Id);
            var update = Builders<Recepe>.Update.Set("Name", recepe.Name);
            update = Builders<Recepe>.Update.Set("Ingredients", recepe.Ingredients);
            var results = await _context.RecepeCollection.UpdateOneAsync(filter,update);
            return await GetRecepe(recepe.Id);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteRecepe(string recepeId) {
        await _context.RecepeCollection.DeleteOneAsync(r => r.Id == recepeId);
    }


}