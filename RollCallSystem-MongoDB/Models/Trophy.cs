using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RollCallSystem_MongoDB.Models;

public class Trophy
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("automatic")]
    public bool Automatic { get; set; }
}
