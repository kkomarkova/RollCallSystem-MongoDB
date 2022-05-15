using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RollCallSystem_MongoDB.Models;

namespace RollCallSystem_MongoDB.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _UsersCollection;

        public UsersService(
            IOptions<RollCallDatabaseSettings> RollCallDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                RollCallDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                RollCallDatabaseSettings.Value.DatabaseName);

            _UsersCollection = mongoDatabase.GetCollection<User>(
                RollCallDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _UsersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _UsersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _UsersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _UsersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _UsersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
