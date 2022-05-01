namespace backend_dev_project.GraphQL.Recepes;

public class RecepeType : ObjectType<Recepe> {
    protected override void Configure(IObjectTypeDescriptor<Recepe> descriptor)
    {
        descriptor.Description("Recepe object in database");
        descriptor.Field(f => f.Name).Description("Name of the recepe");
        descriptor.Field(f => f.Ingredients).Description("All the ingredients of the recepe");
    }

}