var builder = WebApplication.CreateBuilder(args);

var dbSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(dbSettings);

var authSettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthenticationSettings>(authSettings);

builder.Services.AddSingleton<IMongoContext, MongoContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRecepeRepository, RecepeRepository>();
builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();
builder.Services.AddTransient<IUtensilRepository, UtensilRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddValidatorsFromAssemblyContaining<RecepeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RecepeInputValidator>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<Query>()
    .AddType<RecepeType>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();


builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
    options.TokenValidationParameters = new () {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
        ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
    };
});
builder.Services.AddAuthorization();


var app = builder.Build();



app.MapGraphQL();
app.UseGraphQLVoyager(new VoyagerOptions() {
    GraphQLEndPoint = "/GraphQL/"
}, "/graphql-voyager");

app.MapSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", [Authorize] () => "Hello World!");

app.MapGet("/recepes", [Authorize] async (IRecepeRepository recepeService) => {
    var recepes = await recepeService.GetRecepes();
    return Results.Ok(recepes);
});

app.MapGet("/recepes/{recepeId}", [Authorize] async (IRecepeRepository recepeService, string recepeId) => {
    return Results.Ok(await recepeService.GetRecepe(recepeId));
});

app.MapPost("/recepes", [Authorize] async (IValidator<Recepe> validator, Recepe recepe, IRecepeRepository recepeService) => {

    var validationResults = validator.Validate(recepe);
    if (!(validationResults.IsValid)) {
        var errors = validationResults.Errors.Select(err => new { errors = err.ErrorMessage});
        return Results.BadRequest(errors);
    }
    var r = await recepeService.AddRecepe(recepe);
    return Results.Created($"/recepes/{recepe.Id}",r);
});

app.MapPut("/recepes", [Authorize] async (IValidator<Recepe> validator, Recepe recepe, IRecepeRepository recepeService) => {

    var validationResults = validator.Validate(recepe);
    if (!(validationResults.IsValid)) {
        var errors = validationResults.Errors.Select(err => new { errors = err.ErrorMessage});
        return Results.BadRequest(errors);
    }
    var r = await recepeService.UpdateRecepe(recepe);
    return Results.Created($"/recepes/{recepe.Id}",r);
});

app.MapDelete("/recepes/{recepeId}", [Authorize] async (IRecepeRepository recepeService, string recepeId) => {
    await recepeService.DeleteRecepe(recepeId);
    return Results.Ok();
});

app.MapGet("/ingredients", [Authorize] async (IIngredientRepository ingredientRepository) => {
    var ingredients = await ingredientRepository.GetIngredients();
    return Results.Ok(ingredients);
});

app.MapGet("/utensils", [Authorize] async (IUtensilRepository utensilRepository) => {
    var utensils = await utensilRepository.GetUtensils();
    return Results.Ok(utensils);
});

app.MapPost("/authenticate", async (IAuthenticationService authenticationService, AuthenticationRequestBody user, IOptions<AuthenticationSettings> authSettings) => {
    var _settings = authSettings.Value;
    var valid = await authenticationService.ValidateUser(user.username, user.password);
    if (valid == null) {
        return Results.BadRequest();
    }

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_settings.SecretForKey));

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();

    claimsForToken.Add(new Claim("sub", valid.id));
    claimsForToken.Add(new Claim("nickname", valid.username));
    claimsForToken.Add(new Claim("email", valid.email));

    var jwtSecurityToken = new JwtSecurityToken(
        _settings.Issuer,
        _settings.Audience,
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        signingCredentials
    );

    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);




    return Results.Ok(tokenToReturn);
});

app.Run();
public partial class Program { }
