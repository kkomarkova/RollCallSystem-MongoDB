﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RollCallSystem_MongoDB.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("password")]
    public string Password { get; set; }
    [BsonElement("firstName")]
    public string FirstName { get; set; }
    [BsonElement("lastName")]
    public string LastName { get; set; }
    [BsonElement("role")]
    public string Role { get; set; }
    [BsonElement("subjects")]
    public List<UserSubjects> Subjects { get; set; }
    [BsonElement("trophies")]
    public List<UserTrophies> Trophies { get; set; }
}
public class UserSubjects
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}
public class UserTrophies
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}
