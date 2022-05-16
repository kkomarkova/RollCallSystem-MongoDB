using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RollCallSystem_MongoDB.Models;
namespace RollCallSystem_MongoDB.Services;

public class LessonsService
{
    private readonly IMongoCollection<Lesson> _LessonsCollection;

    public LessonsService(
        IOptions<RollCallDatabaseSettings> RollCallDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            RollCallDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            RollCallDatabaseSettings.Value.DatabaseName);

        _LessonsCollection = mongoDatabase.GetCollection<Lesson>(
            RollCallDatabaseSettings.Value.LessonsCollectionName);
    }

    public async Task<List<Lesson>> GetAsync() =>
        await _LessonsCollection.Find(_ => true).ToListAsync();

    public async Task<Lesson?> GetAsync(string id) =>
        await _LessonsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Lesson newLesson) =>
        await _LessonsCollection.InsertOneAsync(newLesson);

    public async Task UpdateAsync(string id, Lesson updatedLesson) =>
        await _LessonsCollection.ReplaceOneAsync(x => x.Id == id, updatedLesson);

    public async Task RemoveAsync(string id) =>
        await _LessonsCollection.DeleteOneAsync(x => x.Id == id);
}
