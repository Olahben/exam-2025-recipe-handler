using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Infrastructure.MongoDbPersistence.Documents;

public class PlaylistDocument
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public required Guid PlaylistId { get; set; }
    public required string Name { get; set; }
    [BsonRepresentation(BsonType.String)]
    public List<Guid> SongIds { get; set; } = new();
    public required List<string> Occasions { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
