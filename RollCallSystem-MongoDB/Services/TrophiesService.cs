using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RollCallSystem_MongoDB.Models;

namespace RollCallSystem_MongoDB.Services;

public class TrophiesService
{
    private readonly IMongoCollection<Trophy> _TrophiesCollection;

    public TrophiesService(
        IOptions<RollCallDatabaseSettings> RollCallDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            RollCallDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            RollCallDatabaseSettings.Value.DatabaseName);

        _TrophiesCollection = mongoDatabase.GetCollection<Trophy>(
            RollCallDatabaseSettings.Value.TrophiesCollectionName);
    }

    public async Task<List<Trophy>> GetAsync() =>
        await _TrophiesCollection.Find(_ => true).ToListAsync();

    public async Task<Trophy?> GetAsync(string id) =>
        await _TrophiesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Trophy newTrophy) =>
        await _TrophiesCollection.InsertOneAsync(newTrophy);

    public async Task UpdateAsync(string id, Trophy updatedTrophy) =>
        await _TrophiesCollection.ReplaceOneAsync(x => x.Id == id, updatedTrophy);

    public async Task RemoveAsync(string id) =>
        await _TrophiesCollection.DeleteOneAsync(x => x.Id == id);
}

