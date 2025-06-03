using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHandler.Infrastructure.MongoDbPersistence.Documents;

public class RecipeDocument
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public required Guid RecipeId { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required List<string> TasteProfile { get; set; }
    public required List<string> Ingredients { get; set; }
    public required List<string> Instructions { get; set; }
    public required TimeOnly PreparationTime { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
