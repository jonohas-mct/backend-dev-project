namespace backend_dev_project.GraphQL.Recepes;

public record AddRecepeInput(string Name, List<IngredientAmount> Ingredients, List<string> Steps, int DurationMinutes, List<string> favorites);