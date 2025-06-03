using MongoDB.Bson;
using MongoDB.Driver;
using RecipeHandler.Infrastructure.MongoDbPersistence.Documents;

namespace RecipeHandler.Infrastructure.MongoDbPersistence;

public class RecipesRepository(IMongoDatabase db)
{
    private IMongoCollection<RecipeDocument> _recipesCollection => db.GetCollection<RecipeDocument>(CollectionNames.Recipes);

    public async Task Insert(
        Guid recipeId,
        string name,
        string category,
        IEnumerable<string> tasteProfile,
        IEnumerable<string> ingredients,
        IEnumerable<string> instructions,
        TimeOnly preparationTime,
        CancellationToken cancellationToken)
    {
        var collection = _recipesCollection;
        var now = DateTime.UtcNow;

        var recipeDocument = new RecipeDocument
        {
            Id = ObjectId.GenerateNewId(),
            RecipeId = recipeId,
            Name = name,
            Category = category,
            TasteProfile = tasteProfile.ToList(),
            Ingredients = ingredients.ToList(),
            Instructions = instructions.ToList(),
            PreparationTime = preparationTime,
        };

        await collection.InsertOneAsync(
            recipeDocument,
            new InsertOneOptions(),
            cancellationToken);
    }

    public async Task<(long count, List<RecipeDocument>)> GetAll(
        IEnumerable<Guid>? recipeIds = null,
        IEnumerable<string>? categories = null,
        IEnumerable<string>? tasteProfiles = null,
        int? skip = 0,
        int? limit = 100,
        CancellationToken cancellationToken = default)
    {
        var collection = _recipesCollection;

        var clauses = new List<FilterDefinition<RecipeDocument>>();
        var filterBuilder = new FilterDefinitionBuilder<RecipeDocument>();

        if (recipeIds != null && recipeIds.Any())
        {
            var songIdFilter = filterBuilder.In(x => x.RecipeId, recipeIds);
            clauses.Add(songIdFilter);
        }

        if (categories != null && categories.Any())
        {
            var genreFilter = filterBuilder.In(x => x.Category, categories);
            clauses.Add(genreFilter);
        }

        if (tasteProfiles != null && tasteProfiles.Any())
        {
            var tagFilter = filterBuilder.AnyIn(x => x.TasteProfile, tasteProfiles);
            clauses.Add(tagFilter);
        }

        var options = new FindOptions<RecipeDocument>
        {
            Skip = skip,
            Limit = limit,
            Sort = Builders<RecipeDocument>.Sort.Descending(x => x.Created)
        };

        var filter = filterBuilder.And(clauses);
        // To avoid errors when no filters are applied, we need to set the filter to empty
        if (clauses.Count == 0)
            filter = filterBuilder.Empty;

        var countTask = collection.CountDocumentsAsync(
            filter,
            null,
            cancellationToken: cancellationToken);

        var getTask = collection.FindAsync(
            filter,
            options,
            cancellationToken: cancellationToken);

        await Task.WhenAll(countTask, getTask);

        return (countTask.Result, getTask.Result.ToList());
    }
}
