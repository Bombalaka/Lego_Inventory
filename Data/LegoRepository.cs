using Lego_Inventory.Data;
using Lego_Inventory.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lego_Inventory.Data 
{
    public class LegoRepository : ILegoRepository
    {
        private readonly IMongoCollection<Legoset> _collection= null!;
        private readonly List<Legoset> _inMemoryStorage = new List<Legoset>();
        private readonly bool _UseInMemory;


        public LegoRepository(IConfiguration configuration)

        {
            
            try
            {
                // Read connection settings from appsettings.json
                var connnectionString = configuration["COSMOSDB_CONNECTIONSTRING"];
                var databaseName = configuration["COSMOSDB_DATABASE"] ?? "LegoInventory";

                var client = new MongoClient(connnectionString);
                 // **Test if MongoDB is reachable**
                var pingCommand = new BsonDocument("ping", 1);
                client.GetDatabase(databaseName).RunCommand<BsonDocument>(pingCommand);

                var database = client.GetDatabase(databaseName);
                _collection = database.GetCollection<Legoset>("legoset");

                Console.WriteLine("✅ Connected to MongoDB!");

                //simple test to check if mongobd is working
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Failed to connect to MongoDB. Running in-memory instead.");
                Console.WriteLine($"Error: {ex.Message}");
                //if any error occurs, use in memory storage
                _UseInMemory = true;
            }
        }
        public async Task<List<Legoset>> GetAllAsync()
        {
            if (_UseInMemory)
            {
                return _inMemoryStorage;
            }
            else
            {
                return await _collection.Find(_ => true).ToListAsync();
            }
        }
        public async Task AddAsync(Legoset legoset)
        {
            if (_UseInMemory)
            {
                // In memory: generate a fake ID and add to list.
                legoset.Id = Guid.NewGuid().ToString();
                _inMemoryStorage.Add(legoset);
            }
            else
            {
                await _collection.InsertOneAsync(legoset);
            }
        }
        public async Task<Legoset> GetByIdAsync(string id)
        {
            if (_UseInMemory)
            {
                return _inMemoryStorage.Find(x => x.Id == id)!;
            }
            else
            {
                var objectId = new ObjectId(id);
                return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
        }
        public async Task UpdateAsync(Legoset legoSet)
        {
            if (_UseInMemory)
            {
                var existing = _inMemoryStorage.Find(x => x.Id == legoSet.Id);
                if (existing != null)
                {
                    existing.Name = legoSet.Name;
                    existing.Description = legoSet.Description;
                }
            }
            else
            {
                await _collection.ReplaceOneAsync(x => x.Id == legoSet.Id, legoSet);
            }
        }
    }
}