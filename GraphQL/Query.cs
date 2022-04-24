
namespace backend_dev_project.GraphQL;
public class Query
{
    [UseFiltering]
    [UseSorting]
    public async Task<List<Recepe>> GetRecepes([Service] IRecepeService recepeServive) {
        return await recepeServive.GetRecepes();
    }


    public string SayHello(string? name = "Jonas"){
        return $"Hello {name}";
    }
}
