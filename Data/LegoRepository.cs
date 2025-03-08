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
                var connnectionString = configuration["MongoDb:ConnectionString"];
                var databaseName = configuration["MongoDb:DatabaseName"];

                var client = new MongoClient(connnectionString);
                 // **Test if MongoDB is reachable**
                var pingCommand = new BsonDocument("ping", 1);
                client.GetDatabase(databaseName).RunCommand<BsonDocument>(pingCommand);

                var database = client.GetDatabase(databaseName);
                _collection = database.GetCollection<Legoset>("legosets");

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
    }
}