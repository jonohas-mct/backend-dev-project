
namespace backend_dev_project.GraphQL;
public class Query
{
    [UseFiltering]
    [UseSorting]
    public async Task<List<Recepe>> GetRecepes([Service] IRecepeService recepeServive) {
        return await recepeServive.GetRecepes();
    }

    [UseFiltering]
    [UseSorting]
    public async Task<List<Ingredient>> GetIngredients([Service] IIngredientRepository ingredientRepository) {
        return await ingredientRepository.GetIngredients();
    }
    
    [UseFiltering]
    [UseSorting]
    public async Task<List<Utensil>> GetUtensils([Service] IUtensilRepository utensilRepository) {
        return await utensilRepository.GetUtensils();
    }


}
