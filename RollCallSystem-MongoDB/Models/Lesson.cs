using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RollCallSystem_MongoDB.Models;

public class Lesson
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("subject")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Subject { get; set; }
    [BsonElement("campus")]
    public string Campus { get; set; }
    [BsonElement("startTime")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime StartTime { get; set; }
    [BsonElement("code")]
    public int Code { get; set; }
    [BsonElement("codeTime")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime codeTime { get; set; }

    [BsonElement("students")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Students { get; set; }


}
