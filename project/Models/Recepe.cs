

namespace backend_dev_project.Models;
public class Recepe {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<IngredientAmount>? Ingredients { get; set; }
    public List<string>? Steps { get; set; }
    public int DurationMinutes { get; set; }
    public List<Utensil> Utensils { get; set; }
}

