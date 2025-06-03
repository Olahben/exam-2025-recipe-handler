using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Testcontainers.MongoDb;
using Xunit;

using System.Threading.Tasks;

namespace RecipesHandler.Tests.Factories;

public class MongoDatabaseFactory : IDisposable, IAsyncLifetime
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly MongoDbContainer _mongoDbContainer;

    public MongoDatabaseFactory()
    {
        _mongoDbContainer = new MongoDbBuilder()
            .Build();

        _mongoDbContainer.StartAsync().Wait();

        var client = new MongoClient(_mongoDbContainer.GetConnectionString());
        _mongoDatabase = client.GetDatabase("IntegrationTestDatabase");
    }

    public IMongoDatabase Create() => _mongoDatabase;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public Task DisposeAsync()
    {
        return _mongoDbContainer.DisposeAsync().AsTask();
    }

    public Task InitializeAsync()
    {
        return _mongoDbContainer.StartAsync();
    }
}
