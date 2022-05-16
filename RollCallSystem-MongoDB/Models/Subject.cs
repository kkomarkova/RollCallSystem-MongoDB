using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RollCallSystem_MongoDB.Models;

public class Subject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("teacher_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Teacher_id { get; set; }

    [BsonElement("students")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Students { get; set; }
}
