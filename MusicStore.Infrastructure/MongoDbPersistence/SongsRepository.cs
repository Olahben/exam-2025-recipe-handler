using MongoDB.Bson;
using MongoDB.Driver;
using MusicStore.Infrastructure.MongoDbPersistence.Documents;

namespace MusicStore.Infrastructure.MongoDbPersistence;

public class SongsRepository(IMongoDatabase db)
{
    private IMongoCollection<SongDocument> _songsCollection => db.GetCollection<SongDocument>(CollectionNames.Songs);

    public async Task Insert(
        Guid songId,
        string genre,
        List<string> tags,
        CancellationToken cancellationToken)
    {
        var collection = _songsCollection;
        var now = DateTime.UtcNow;

        var songDocument = new SongDocument
        {
            Id = ObjectId.GenerateNewId(),
            SongId = songId,
            Genre = genre,
            Tags = tags,
            Created = now,
            ModifiedAt = now
        };

        await collection.InsertOneAsync(
            songDocument,
            new InsertOneOptions(),
            cancellationToken);
    }

    public async Task<(long count, List<SongDocument>)> GetAll(
        IEnumerable<Guid>? songIds = null,
        IEnumerable<string>? genres = null,
        IEnumerable<string>? tags = null,
        int? skip = 0,
        int? limit = 100,
        CancellationToken cancellationToken = default)
    {
        var collection = _songsCollection;

        var clauses = new List<FilterDefinition<SongDocument>>();
        var filterBuilder = new FilterDefinitionBuilder<SongDocument>();

        if (songIds != null && songIds.Any())
        {
            var songIdFilter = filterBuilder.In(x => x.SongId, songIds);
            clauses.Add(songIdFilter);
        }

        if (genres != null && genres.Any())
        {
            var genreFilter = filterBuilder.In(x => x.Genre, genres);
            clauses.Add(genreFilter);
        }

        if (tags != null && tags.Any())
        {
            var tagFilter = filterBuilder.AnyIn(x => x.Tags, tags);
            clauses.Add(tagFilter);
        }

        var options = new FindOptions<SongDocument>
        {
            Skip = skip,
            Limit = limit,
            Sort = Builders<SongDocument>.Sort.Descending(x => x.Created)
        };

        var filter = filterBuilder.And(clauses);
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
