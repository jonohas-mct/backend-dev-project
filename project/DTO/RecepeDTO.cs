namespace backend_dev_project.DTO;
public class RecepeDTO
{
    public string? Name { get; set; }
    public List<IngredientAmount>? Ingredients { get; set; }
}