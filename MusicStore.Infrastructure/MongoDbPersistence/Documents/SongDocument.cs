using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicStore.Infrastructure.MongoDbPersistence.Documents;

public class SongDocument
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid SongId { get; set; }
    public required string Genre { get; set; }
    public required List<string> Tags { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
