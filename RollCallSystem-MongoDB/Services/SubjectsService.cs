using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RollCallSystem_MongoDB.Models;

namespace RollCallSystem_MongoDB.Services;

public class SubjectsService
{
    private readonly IMongoCollection<Subject> _SubjectsCollection;

    public SubjectsService(
        IOptions<RollCallDatabaseSettings> RollCallDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            RollCallDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            RollCallDatabaseSettings.Value.DatabaseName);

        _SubjectsCollection = mongoDatabase.GetCollection<Subject>(
            RollCallDatabaseSettings.Value.SubjectsCollectionName);
    }

    public async Task<List<Subject>> GetAsync() =>
        await _SubjectsCollection.Find(_ => true).ToListAsync();

    public async Task<Subject?> GetAsync(string id) =>
        await _SubjectsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Subject newSubject) =>
        await _SubjectsCollection.InsertOneAsync(newSubject);

    public async Task UpdateAsync(string id, Subject updatedSubject) =>
        await _SubjectsCollection.ReplaceOneAsync(x => x.Id == id, updatedSubject);

    public async Task RemoveAsync(string id) =>
        await _SubjectsCollection.DeleteOneAsync(x => x.Id == id);
}
