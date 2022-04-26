namespace backend_dev_project.Repositories;
public class FakeRecepeRepository : IRecepeRepository {
    public static List<Recepe> _recepes = new List<Recepe>();

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
            DurationMinutes = 15,
            Steps = new List<string> () {
                "Zeef de bloem met het zout boven een grote beslagkom.",
                "Voeg de eiren en de helft van de melk toe.",
                "Klop met een garde tot een glad beslag.",
                "Schenk de rest van de melk erbij en klop opnieuw glad.",
                "Dek af met vershoudfolie en laat het beslag 30 min. staan.",
                "Verhit een klontje boter in een koekenpan van 20 cm doorsnee en schep er een soeplepel beslag in.",
                "Draai de pan rond zodat de hele bodem bedekt is.",
                "Laat de pannekoek 2 min. op middelhoog vuur bakken tot de bovenkant droog is en de onderkant goudbruin.",
                "Draai de pannenkoek om en bak nog 1 min.",
                "Herhaal met de rest van het beslag"
            },
            Utensils = new List<Utensil>() {
                new Utensil() {Name = "Beslagkom"},
                new Utensil() {Name = "Zeef"},
                new Utensil() {Name = "Garde"},
                new Utensil() {Name = "Vershoudfolie"},
                new Utensil() {Name = "Soeplepel"},
                new Utensil() {Name = "Pan"},
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
                "Zeef de bloem met het zout boven een grote beslagkom.",
                "Voeg de eiren en de helft van de melk toe.",
                "Klop met een garde tot een glad beslag.",
                "Schenk de rest van de melk erbij en klop opnieuw glad.",
                "Dek af met vershoudfolie en laat het beslag 30 min. staan.",
                "Verhit een klontje boter in een koekenpan van 20 cm doorsnee en schep er een soeplepel beslag in.",
                "Draai de pan rond zodat de hele bodem bedekt is.",
                "Laat de pannekoek 2 min. op middelhoog vuur bakken tot de bovenkant droog is en de onderkant goudbruin.",
                "Draai de pannenkoek om en bak nog 1 min.",
                "Herhaal met de rest van het beslag"
            },
            Utensils = new List<Utensil>() {
                new Utensil() {Name = "Beslagkom"},
                new Utensil() {Name = "Zeef"},
                new Utensil() {Name = "Garde"},
                new Utensil() {Name = "Vershoudfolie"},
                new Utensil() {Name = "Soeplepel"},
                new Utensil() {Name = "Pan"},
            }
        });

    }

    public async Task<List<Recepe>> GetRecepes () {
        return _recepes;
    }

    public async Task<Recepe> GetRecepe (string recepeId) {
        Recepe r = _recepes.FirstOrDefault(e => e.Id == recepeId);
        return r;
    }

    public async Task<Recepe> AddRecepe (Recepe recepe) {
        Recepe r = _recepes.FirstOrDefault(e => e.Name == recepe.Name);
        if (r != null) {
            return null;
        }
        _recepes.Add(recepe);
        return recepe;
    }

    public async Task<Recepe> UpdateRecepe( Recepe recepe) {
        Recepe r = _recepes.FirstOrDefault(e => e.Name == recepe.Name);
        if (r != null) {
            return null;
        }
        Recepe update = _recepes.FirstOrDefault(e => e.Id == recepe.Id);
        update.Ingredients = recepe.Ingredients;
        update.Name = recepe.Name;
        update.Steps = recepe.Steps;
        update.Utensils = recepe.Utensils;
        update.DurationMinutes = recepe.DurationMinutes;

        return update;
    }

    public async Task<bool> DeleteRecepe(string recepeId) {
        Recepe recepeToRemove = _recepes.FirstOrDefault(r => r.Id == recepeId);
        if (recepeToRemove == null) {
            return false;
        }
        _recepes.Remove(recepeToRemove);
        return true;
    }


}