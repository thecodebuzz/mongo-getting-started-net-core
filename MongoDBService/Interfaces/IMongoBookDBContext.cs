using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using NoSQL.GenericRepository.Model;

namespace NoSQL.GenericRepository.Interfaces
{
    public interface IMongoBookDBContext
    {
        IMongoCollection<Book> GetCollection<Book>(string name);
        IMongoCollection<Book> BookCollection { get; }
    }
}