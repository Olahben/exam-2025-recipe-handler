using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RecipeHandler.Infrastructure.MongoDbPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesHandler.Tests.Factories;

public class TestFixture
{
    public IServiceProvider ServiceProvider { get; private set; }
    public TestFixture()
    {
        var services = new ServiceCollection();

        services.AddSingleton<RecipesRepository>();
        services.AddSingleton<MenusRepository>();

        services.AddSingleton(_ => CreateMongoDatabase());

        var assemblies = new[]
        {
            typeof(RecipeHandler.AssemblyMarker).Assembly,
            typeof(RecipeHandler.Infrastructure.AssemblyMarker).Assembly
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        services.AddMemoryCache();

        ServiceProvider = services.BuildServiceProvider();
    }

    // This method is used to create a MongoDB database instance for testing purposes.
    private IMongoDatabase CreateMongoDatabase()
    {
        var f = new MongoDatabaseFactory();
        var mongoDatabase = f.Create();

        return mongoDatabase;
    }

    public T Get<T>() where T : class
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}
