using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSQL.GenericRepository.Interfaces;
using NoSQL.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoSQL.GenericRepository.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMongoBookDBContext _context;
        protected IMongoCollection<Book> _dbCollection;
        public BookController(IMongoBookDBContext context)
        {
            _context = context;
            _dbCollection= _context.GetCollection<Book>(typeof(Book).Name);

        }
        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var all = await _dbCollection.FindAsync(Builders<Book>.Filter.Empty);
            return Ok(all.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(string id)
        {

            if (id == null)
            {
                throw new ArgumentNullException("Input object is null");
            }
            var objectId = new ObjectId(id);
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq("_id", objectId);
            var result = await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

            return Ok(result);
        }

        //[HttpPost]
        //public async IActionResult PostSimulatingError([FromBody] Book value)
        //{
        //    _productRepository.Create(value);

        //    // The product will not be added
        //    return Ok(await _productRepository.Get(value.Id));
        //}

        //[HttpPost]
        //public async Task<ActionResult<Book>> Post([FromBody] Book value)
        //{
        //    _productRepository.Create(value);

        //    return Ok(await _productRepository.Get(value.Id));
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Book>> Put(ObjectId id, [FromBody] Book value)
        //{

        //    _productRepository.Update(value);

        //    return Ok(await _productRepository.Get(value.Id));
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(string id)
        //{
        //    _productRepository.Delete(id);

        //    return Ok();
        //}
    }
}
