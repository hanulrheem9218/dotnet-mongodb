using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MongoExample.Services
{

    public class MongoDBService
    {
        private readonly IMongoCollection<PlayList>? _playerListCollection;
        private readonly IConfiguration _configuration;
        public MongoDBService(IConfiguration configuration )
        {
            _configuration = configuration;
            var url = MongoUrl.Create(_configuration.GetConnectionString("ConnectionURI"));
            MongoClient client = new MongoClient(url);
            IMongoDatabase database = client.GetDatabase(_configuration.GetConnectionString("DatabaseName"));
            _playerListCollection = database.GetCollection<PlayList>(_configuration.GetConnectionString("CollectionName"));

        }

        public async Task CreateAsync(PlayList playlist)
        {
            await _playerListCollection!.InsertOneAsync(playlist);
            return;

        }

        public async Task<List<PlayList>> GetAsync()
        {
            return await _playerListCollection.Find(new BsonDocument()).ToListAsync();

        }

        public async Task AddToPlayListAsync(string id, string movieId)
        {
            FilterDefinition<PlayList> filter = Builders<PlayList>.Filter.Eq("Id", id);
            UpdateDefinition<PlayList> update = Builders<PlayList>.Update.AddToSet<string>("movieId", movieId);
            await _playerListCollection!.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsybc(string id)
        {
            FilterDefinition<PlayList> filter = Builders<PlayList>.Filter.Eq("Id", id);
            await _playerListCollection!.DeleteOneAsync(filter);
            return;
        }

    }
}