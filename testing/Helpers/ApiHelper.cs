public class Helper
{

    public static WebApplicationFactory<Program> CreateApi()
    {
        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IRecepeRepository));
                services.Remove(descriptor);

                var fakeRecepeRepository = new ServiceDescriptor(typeof(IRecepeRepository), typeof(FakeRecepeRepository), ServiceLifetime.Transient);
                services.Add(fakeRecepeRepository);

                var descriptorInrgedient = services.SingleOrDefault(d => d.ServiceType == typeof(IIngredientRepository));
                services.Remove(descriptorInrgedient);

                var fakeIngredientRepository = new ServiceDescriptor(typeof(IIngredientRepository), typeof(FakeIngredientRepository), ServiceLifetime.Transient);
                services.Add(fakeIngredientRepository);

                var descriptorUtensil = services.SingleOrDefault(d => d.ServiceType == typeof(IUtensilRepository));
                services.Remove(descriptorUtensil);

                var fakeUtensilRepository = new ServiceDescriptor(typeof(IUtensilRepository), typeof(FakeUtensilRepository), ServiceLifetime.Transient);
                services.Add(fakeUtensilRepository);

                var descriptorUser = services.SingleOrDefault(d => d.ServiceType == typeof(IUserRepository));
                services.Remove(descriptorUser);

                var fakeUserRepository = new ServiceDescriptor(typeof(IUserRepository), typeof(FakeUserRepository), ServiceLifetime.Transient);
                services.Add(fakeUserRepository);
            });
        });

        return application;

    }
}