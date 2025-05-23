using MongoDB.Bson;
using MongoDB.Driver;
using MusicStore.Infrastructure.MongoDbPersistence.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Infrastructure.MongoDbPersistence;

public class PlaylistsRepository(IMongoDatabase db)
{
    private IMongoCollection<PlaylistDocument> _playlistsCollection => db.GetCollection<PlaylistDocument>(CollectionNames.Playlists);
    public async Task Insert(
        Guid playlistId,
        string name,
        List<Guid> songIds,
        List<string> occasions,
        CancellationToken cancellationToken)
    {
        var collection = _playlistsCollection;
        var now = DateTime.UtcNow;
        var playlistDocument = new PlaylistDocument
        {
            Id = ObjectId.GenerateNewId(),
            PlaylistId = playlistId,
            Name = name,
            SongIds = songIds,
            Occasions = occasions,
            Created = now,
            ModifiedAt = now
        };
        await collection.InsertOneAsync(
            playlistDocument,
            new InsertOneOptions(),
            cancellationToken);
    }

    public async Task<(long count, List<PlaylistDocument>)> GetAll(
        IEnumerable<Guid>? playlistIds = null,
        IEnumerable<string>? names = null,
        IEnumerable<string>? occasions = null,
        int? skip = 0,
        int? limit = 100,
        CancellationToken cancellationToken = default)
    {
        var collection = _playlistsCollection;
        var clauses = new List<FilterDefinition<PlaylistDocument>>();
        var filterBuilder = new FilterDefinitionBuilder<PlaylistDocument>();

        if (playlistIds != null && playlistIds.Any())
        {
            var playlistIdFilter = filterBuilder.In(x => x.PlaylistId, playlistIds);
            clauses.Add(playlistIdFilter);
        }
        if (names != null && names.Any())
        {
            var nameFilter = filterBuilder.In(x => x.Name, names);
            clauses.Add(nameFilter);
        }
        if (occasions != null && occasions.Any())
        {
            var occasionFilter = filterBuilder.AnyIn(x => x.Occasions, occasions);
            clauses.Add(occasionFilter);
        }

        var filter = filterBuilder.And(clauses);
        // To avoid errors when no filters are applied, we need to set the filter to empty
        if (clauses.Count == 0)
        {
            filter = filterBuilder.Empty;
        }

        var countTask = collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var itemsTask = collection
            .Find(filter)
            .Skip(skip ?? 0)
            .Limit(limit ?? 100)
            .ToListAsync(cancellationToken);

        await Task.WhenAll(countTask, itemsTask);
        return (countTask.Result, itemsTask.Result);
    }
}
