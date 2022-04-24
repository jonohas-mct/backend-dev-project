namespace backend_dev_project.Models;
public class IngredientAmount {
    public Ingredient? Ingredient { get; set; }
    public int Amount { get; set; }
    public string? Unit { get; set; }
}