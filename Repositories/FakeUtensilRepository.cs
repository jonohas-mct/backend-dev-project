namespace backend_dev_project.Repositories;


public class FakeUtensilRepository : IUtensilRepository {

    private static List<Utensil> _utensils = new List<Utensil>();

    public FakeUtensilRepository () {

        _utensils.Add( new Utensil() {
            Id = "1",
            Name = "Soeplepel"
        });

        _utensils.Add( new Utensil() {
            Id = "2",
            Name = "Eetlepel"
        });

    }

    public async Task<List<Utensil>> GetUtensils () {
        return _utensils;
    }

}