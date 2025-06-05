using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeHandler.Infrastructure.MongoDbPersistence.Documents;

public class MenuDocument
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public required Guid MenuId { get; set; }
    public required string Name { get; set; }
    public required List<string> Occasions { get; set; }

    [BsonRepresentation(BsonType.String)]
    public required List<Guid> RecipeIds { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
