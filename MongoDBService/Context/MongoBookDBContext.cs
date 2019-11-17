using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSQL.GenericRepository.Interfaces;
using NoSQL.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoSQL.GenericRepository.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoBookDBContext : IMongoBookDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }

        public IClientSessionHandle Session { get; set; }

        public IMongoCollection<Book> BookCollection => _db.GetCollection<Book>("Book");

        public MongoBookDBContext(IOptions<Mongosettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.Connection);

            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }
      
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return null;
            }
            return _db.GetCollection<T>(name);
        }
    }
}
