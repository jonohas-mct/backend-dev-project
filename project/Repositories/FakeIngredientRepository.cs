namespace backend_dev_project.Repositories;


public class FakeIngredientRepository : IIngredientRepository {

    private static List<Ingredient> _ingredients = new List<Ingredient>();

    public FakeIngredientRepository () {

        _ingredients.Add( new Ingredient() {
            Id = "1",
            Name = "Suiker"
        });

        _ingredients.Add( new Ingredient() {
            Id = "2",
            Name = "Bananen"
        });

    }

    public async Task<List<Ingredient>> GetIngredients () {
        return _ingredients;
    }

}