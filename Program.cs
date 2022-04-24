var builder = WebApplication.CreateBuilder(args);

var dbSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(dbSettings);
builder.Services.AddSingleton<IMongoContext, MongoContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRecepeRepository, RecepeRepository>();
builder.Services.AddTransient<IRecepeService, RecepeService>();
builder.Services.AddValidatorsFromAssemblyContaining<RecepeValidator>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<Query>()
    .AddType<RecepeType>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();


var app = builder.Build();

app.MapGraphQL();
app.UseGraphQLVoyager(new VoyagerOptions() {
    GraphQLEndPoint = "/GraphQL/"
}, "/graphql-voyager");

app.MapSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapGet("/recepes", async (IRecepeService recepeService) => {
    var recepes = await recepeService.GetRecepes();
    return Results.Ok(recepes);
});

app.MapGet("/recepes/{recepeId}", async (IRecepeService recepeService, string recepeId) => {
    return Results.Ok(await recepeService.GetRecepe(recepeId));
});

app.MapPost("/recepes", async (IValidator<Recepe> validator, Recepe recepe, IRecepeService recepeService) => {

    var validationResults = validator.Validate(recepe);
    if (!(validationResults.IsValid)) {
        var errors = validationResults.Errors.Select(err => new { errors = err.ErrorMessage});
        return Results.BadRequest(errors);
    }
    var r = await recepeService.AddRecepe(recepe);
    return Results.Created($"/recepes/{recepe.Id}",r);
});

app.MapPut("/recepes", async (IValidator<Recepe> validator, Recepe recepe, IRecepeService recepeService) => {

    var validationResults = validator.Validate(recepe);
    if (!(validationResults.IsValid)) {
        var errors = validationResults.Errors.Select(err => new { errors = err.ErrorMessage});
        return Results.BadRequest(errors);
    }
    var r = await recepeService.UpdateRecepe(recepe);
    return Results.Created($"/recepes/{recepe.Id}",r);
});

app.MapDelete("/recepes/{recepeId}", async (IRecepeService recepeService, string recepeId) => {
    await recepeService.DeleteRecepe(recepeId);
    return Results.Ok();
});

app.Run();
