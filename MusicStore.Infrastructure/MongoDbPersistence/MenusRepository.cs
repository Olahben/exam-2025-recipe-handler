using MongoDB.Bson;
using MongoDB.Driver;
using RecipeHandler.Infrastructure.MongoDbPersistence.Documents;

namespace RecipeHandler.Infrastructure.MongoDbPersistence;

public class MenusRepository(IMongoDatabase db)
{
    public IMongoCollection<MenuDocument> _menusCollection => db.GetCollection<MenuDocument>(CollectionNames.Menus);

    public async Task Insert(
        Guid menuId,
        string name,
        IEnumerable<string> occasions,
        IEnumerable<Guid> recipeIds,
        CancellationToken cancellationToken = default)
    {
        var collection = _menusCollection;
        var now = DateTime.UtcNow;

        var menuDocument = new MenuDocument
        {
            Id = ObjectId.GenerateNewId(),
            MenuId = menuId,
            Name = name,
            Occasions = occasions.ToList(),
            RecipeIds = recipeIds.ToList(),
            Created = now,
            ModifiedAt = now
        };

        await collection.InsertOneAsync(
            menuDocument,
            new InsertOneOptions(),
            cancellationToken);
    }

    public async Task<(long, List<MenuDocument>)> GetAll(
        IEnumerable<Guid>? menuIds = null,
        IEnumerable<string>? occasions = null,
        int? skip = 0,
        int? limit = 100,
        CancellationToken cancellationToken = default)
    {
        var collection = _menusCollection;
        var clauses = new List<FilterDefinition<MenuDocument>>();
        var filterBuilder = new FilterDefinitionBuilder<MenuDocument>();
        if (menuIds != null && menuIds.Any())
        {
            var menuIdFilter = filterBuilder.In(x => x.MenuId, menuIds);
            clauses.Add(menuIdFilter);
        }
        if (occasions != null && occasions.Any())
        {
            var occasionFilter = filterBuilder.AnyIn(x => x.Occasions, occasions);
            clauses.Add(occasionFilter);
        }
        var filter = clauses.Count > 0 ? filterBuilder.And(clauses) : filterBuilder.Empty;
        var count = await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        var menus = await collection.Find(filter)
            .Skip(skip ?? 0)
            .Limit(limit ?? 100)
            .ToListAsync(cancellationToken);

        return (count, menus);
    }
}
