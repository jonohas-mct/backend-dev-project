namespace backend_dev_project.Repositories;

public interface IUtensilRepository {
    Task<List<Utensil>> GetUtensils();
}

public class UtensilRepository : IUtensilRepository {
    private readonly IMongoContext _context;

    public UtensilRepository (IMongoContext context) {

        _context = context;

    }

    public async Task<List<Utensil>> GetUtensils () {
        var alles = await _context.UtensilCollection.Find(_ => true).ToListAsync();
        return alles;
    }

}