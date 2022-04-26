namespace backend_dev_project.Repositories;

public interface IRecepeRepository {
    Task<List<Recepe>> GetRecepes();
    Task<Recepe> GetRecepe(string recepeId);
    Task<Recepe> AddRecepe(Recepe recepe);
    Task<Recepe> UpdateRecepe(Recepe recepe);
    Task<bool> DeleteRecepe(string recepeId);
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
        var findRecepe = await _context.RecepeCollection.Find<Recepe>(r => r.Name == recepe.Name).FirstOrDefaultAsync();
        if (findRecepe != null) {
            return null;
        }
        try {
            foreach (IngredientAmount ingredientAmount in recepe.Ingredients)
            {
                var searchIngredient = await _context.IngredientCollection.Find<Ingredient>(r => r.Name == ingredientAmount.Ingredient.Name).FirstOrDefaultAsync();

                if (searchIngredient == null) {
                    await _context.IngredientCollection.InsertOneAsync(ingredientAmount.Ingredient);
                    searchIngredient = await _context.IngredientCollection.Find<Ingredient>(r => r.Name == ingredientAmount.Ingredient.Name).FirstOrDefaultAsync();
                }

                ingredientAmount.Ingredient = searchIngredient;
            }

            for (int i = 0; i < recepe.Utensils.Count; i++)
            {
                var _utensil = recepe.Utensils[i];
                var searchUtensil = await _context.UtensilCollection.Find<Utensil>(u => u.Name == _utensil.Name).FirstOrDefaultAsync();
                if (searchUtensil == null) {
                    await _context.UtensilCollection.InsertOneAsync(_utensil);
                    searchUtensil = await _context.UtensilCollection.Find<Utensil>(u => u.Name == _utensil.Name).FirstOrDefaultAsync();
                }
                recepe.Utensils[i] = searchUtensil;
            }

            await _context.RecepeCollection.InsertOneAsync(recepe);
            return recepe;
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Recepe> UpdateRecepe( Recepe recepe) {
        var findRecepe = await _context.RecepeCollection.Find<Recepe>(r => r.Name == recepe.Name).FirstOrDefaultAsync();
        if (findRecepe != null) {
            return null;
        }
        try {
            var filter = Builders<Recepe>.Filter.Eq("Id", recepe.Id);
            foreach (IngredientAmount ingredientAmount in recepe.Ingredients)
            {
                var searchIngredient = await _context.IngredientCollection.Find<Ingredient>(r => r.Name == ingredientAmount.Ingredient.Name).FirstOrDefaultAsync();

                if (searchIngredient == null) {
                    await _context.IngredientCollection.InsertOneAsync(ingredientAmount.Ingredient);
                    searchIngredient = await _context.IngredientCollection.Find<Ingredient>(r => r.Name == ingredientAmount.Ingredient.Name).FirstOrDefaultAsync();
                }

                ingredientAmount.Ingredient = searchIngredient;
            }

            for (int i = 0; i < recepe.Utensils.Count; i++)
            {
                var _utensil = recepe.Utensils[i];
                var searchUtensil = await _context.UtensilCollection.Find<Utensil>(u => u.Name == _utensil.Name).FirstOrDefaultAsync();
                if (searchUtensil == null) {
                    await _context.UtensilCollection.InsertOneAsync(_utensil);
                    searchUtensil = await _context.UtensilCollection.Find<Utensil>(u => u.Name == _utensil.Name).FirstOrDefaultAsync();
                }
                recepe.Utensils[i] = searchUtensil;
            }
            await _context.RecepeCollection.ReplaceOneAsync(filter, recepe, new ReplaceOptions() {IsUpsert = true});

            return await GetRecepe(recepe.Id);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteRecepe(string recepeId) {
        var findRecepe = await _context.RecepeCollection.Find<Recepe>(r => r.Id == recepeId).FirstOrDefaultAsync();
        if (findRecepe == null) {
            return false;
        }
        await _context.RecepeCollection.DeleteOneAsync(r => r.Id == recepeId);
        return true;
    }


}