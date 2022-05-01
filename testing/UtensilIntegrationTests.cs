namespace Testing;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

public class UtensilIntegrationTests {
    
    private string _token = "";

    public UtensilIntegrationTests() {
        Task.Run(() => GetToken("JonahasFake", "test")).Wait();
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

    [Fact]
    public async Task Should_Return_Utensils() {

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await client.GetAsync("/utensils");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var utensils = await result.Content.ReadFromJsonAsync<List<Utensil>>();
        Assert.NotNull(utensils);
        Assert.True(utensils.Count > 0);
    }
}