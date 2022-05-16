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
    public DateTime CodeTime { get; set; }

    [BsonElement("students")]
    public List<AttendingStudent> Students { get; set; }

}

public class AttendingStudent
{
    [BsonElement("student_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string StudentId { get; set; }

    [BsonElement("attended")]
    public bool Attended { get; set; }
}