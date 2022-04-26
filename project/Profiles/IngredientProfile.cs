
namespace backend_dev_project.Profiles;
public class IngredientProfile : Profile
{
    public IngredientProfile() {
        CreateMap<Ingredient, IngredientDTO>(); 
    }
}
