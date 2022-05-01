namespace backend_dev_project.GraphQL;

public class Mutation {
    public async Task<AddRecepePayload> AddRecepeAsync( AddRecepeInput input, [Service] IRecepeService recepeService, [Service] IValidator<AddRecepeInput> validator) {
        
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid){
            var newRecepe = new Recepe() { Name = input.Name, Ingredients = input.Ingredients, Steps = input.Steps};
            var create = await recepeService.AddRecepe(newRecepe);
            return new AddRecepePayload(create);
        }

        string message = string.Empty;

        foreach( var error in validationResult.Errors) {
            message += error.ErrorMessage;
        }

        throw new Exception(message);
    }
}