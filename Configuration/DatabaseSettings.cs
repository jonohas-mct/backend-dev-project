namespace backend_dev_project.Configuration;

public class DatabaseSettings {
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? RecepeCollection { get; set; }
    public string? IngredientCollection { get; set; }
}