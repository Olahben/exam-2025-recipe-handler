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
}
