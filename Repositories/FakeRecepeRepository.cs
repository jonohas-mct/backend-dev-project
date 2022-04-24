namespace backend_dev_project.Repositories;
public class FakeRecepeRepository : IRecepeRepository {
    private static List<Recepe> _recepes = new List<Recepe>();

    public FakeRecepeRepository () {

        _recepes.Add(new Recepe() {
            Id = "1",
            Name = "Pancakes",
            Ingredients = new List<IngredientAmount>() {
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Suggar"
                    },
                    Amount = 100,
                    Unit = "g"
                },
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Eggs"
                    },
                    Amount = 2
                }
            },
            Steps = new List<string> () {
                "Stir",
                "Eat"
            }
        });

        _recepes.Add(new Recepe() {
            Id = "2",
            Name = "Appelflap",
            Ingredients = new List<IngredientAmount>() {
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Suggar"
                    },
                    Amount = 100,
                    Unit = "g"
                },
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Eggs"
                    },
                    Amount = 2
                },
                new IngredientAmount(){
                    Ingredient = new Ingredient() {
                        Name = "Appels"
                    },
                    Amount = 4
                }
            },
            Steps = new List<string> () {
                "Stir",
                "Eat"
            }
        });

    }

    public async Task<List<Recepe>> GetRecepes () {
        return _recepes;
    }

    public async Task<Recepe> GetRecepe (string recepeId) {
        Recepe r = _recepes.Find(e => e.Id == recepeId);
        return r;
    }

    public async Task<Recepe> AddRecepe (Recepe recepe) {
        _recepes.Add(recepe);

        return recepe;
    }

    public async Task<Recepe> UpdateRecepe( Recepe recepe) {
        Recepe update = _recepes.Find(e => e.Id == recepe.Id);
        update.Ingredients = recepe.Ingredients;
        update.Name = recepe.Name;
        update.Steps = recepe.Steps;

        return update;
    }

    public async Task DeleteRecepe(string recepeId) {
        Recepe recepeToRemove = _recepes.Single(r => r.Id == recepeId);
        _recepes.Remove(recepeToRemove);
    }


}