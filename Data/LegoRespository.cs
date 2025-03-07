using Lego_Inventory.Models;

using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LegoInventory.Data 
{
    public class LegoRepository
    {
        private readonly IMongoCollection<Legoset> _collection;
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
                var database = client.GetDatabase(databaseName);
                _collection = database.GetCollection<Legoset>("legosets");

                //simple test to check if mongobd is working
                _collection.Find(_=>true).FirstOrDefault();
            }
            catch (Exception)
            {
                
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