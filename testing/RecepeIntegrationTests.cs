namespace Testing;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;



public record LoginCredentials (string username, string password);


public class RecepeIntegrationTests
{
    public string _token = "";

    public RecepeIntegrationTests() {
        Task.Run(() => GetToken("JonahasFake", "test")).Wait();
    }


    // Object for testing the recepe add function
    public static IEnumerable<object[]> _recepes() {
        yield return new object[] {
            new Recepe() {
                Id = "3",
                Name = "PancakesAdded",
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
            },
            HttpStatusCode.Created
        };
        yield return new object[] {
            new Recepe() {
                Id = "4",
                Name = "AppelflapAdded",
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
            },
            HttpStatusCode.Created
        };
        yield return new object[] {
            new Recepe() {
                Id = "5",
                Name = "AppelflapAdded",
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
            },
            HttpStatusCode.Conflict
        };
    }

    // Object for testing the recepe update function
    public static IEnumerable<object[]> _recepesUpdate() {
        yield return new object[] {
            new Recepe() {
                Id = "1",
                Name = "PancakesUpdated",
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
            },
            HttpStatusCode.OK
        };
        yield return new object[] {
            new Recepe() {
                Id = "2",
                Name = "AppelflapUpdated",
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
            },
            HttpStatusCode.OK
        };
        yield return new object[] {
            new Recepe() {
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
            },
            HttpStatusCode.Conflict
        };

    }

    // Object for testing the recepe delete function
    public static IEnumerable<object[]> _recepesDelete() {
        yield return new object[] {
            "1",
            HttpStatusCode.OK
        };
        yield return new object[] {
            "2",
            HttpStatusCode.OK
        };
        yield return new object[] {
            "300",
            HttpStatusCode.NotFound
        };
    }
    public async Task<HttpResponseMessage> GetToken(string username, string password) {
        var application = Helper.CreateApi();
        var client = application.CreateClient();


        var user = new LoginCredentials(username, password);

        var content = JsonSerializer.Serialize(user);
        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);

        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var login = await client.PostAsync("/authenticate", byteContent);

        if (login.StatusCode == HttpStatusCode.OK) {
            var token = await login.Content.ReadFromJsonAsync<AuthenticationResponseBody>();
            _token = token.token;
        }
        return login;
    }

    [Theory]
    [InlineData("JonahasFake", "test", HttpStatusCode.OK)]
    [InlineData("JonahasFak", "test", HttpStatusCode.BadRequest)]
    [InlineData("JonahasFake", "tes", HttpStatusCode.BadRequest)]
    public async Task Login_Test(string username, string password, HttpStatusCode expectedValue)
    {
        var login = await GetToken(username, password);
        login.StatusCode.Should().Be(expectedValue);
    }


    [Fact]
    public async Task Should_Return_Recepes() {

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await client.GetAsync("/recepes");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var recepes = await result.Content.ReadFromJsonAsync<List<Recepe>>();
        Assert.NotNull(recepes);
        Assert.True(recepes.Count > 0);
    }

    [Theory]
    [InlineData("1", HttpStatusCode.OK)]
    [InlineData("2", HttpStatusCode.OK)]
    [InlineData("1000", HttpStatusCode.NotFound)]
    public async Task Should_Return_Recepe(string recepeId, HttpStatusCode expectedStatus){
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await client.GetAsync($"/recepes/{recepeId}");
        result.StatusCode.Should().Be(expectedStatus);
    }


    [Theory]
    [MemberData(nameof(_recepes))]
    public async Task Should_Add_Recepes(Recepe recepe, HttpStatusCode expectedStatus){
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var content = JsonSerializer.Serialize(recepe);
        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);

        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var result = await client.PostAsync("/recepes", byteContent);
        result.StatusCode.Should().Be(expectedStatus);
    }

    [Theory]
    [MemberData(nameof(_recepesUpdate))]
    public async Task Should_Update_Recepes(Recepe recepe, HttpStatusCode expectedStatus){
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var content = JsonSerializer.Serialize(recepe);
        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);

        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var result = await client.PutAsync("/recepes", byteContent);
        result.StatusCode.Should().Be(expectedStatus);
    }

    [Theory]
    [MemberData(nameof(_recepesDelete))]
    public async Task Should_Delete_Recepes(string recepeId, HttpStatusCode expectedStatus){
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await client.DeleteAsync($"/recepes/{recepeId}");
        result.StatusCode.Should().Be(expectedStatus);
    }

    
}