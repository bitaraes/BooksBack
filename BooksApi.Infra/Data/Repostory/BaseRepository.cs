using BooksApi.Domain.Entities;
using BooksApi.Domain.Repository;
using BooksApi.Infraestructure.Data.Context;
using BooksApi.Infraestructure.Data.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Infraestructure.Data.Repostory
{
    public class BaseRepository<T> : IRepository<BookEntity> where T : BaseEntity
    {
        public IMongoCollection<BookEntity> _books;

        public BaseRepository(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<BookEntity>(settings.BooksCollectionName);
        }
        
        public BookEntity Create(BookEntity book)
        {
            _books.InsertOne(book);
            return book;
        }
        public List<BookEntity> Get()
        {
           return _books.Find(book => true).ToList();
        }
        public BookEntity Get(string id)
        {
           return _books.Find<BookEntity>(book => book.Id == id).FirstOrDefault();
        }
        public void Remove(string id)
        {
            _books.DeleteOne(book => book.Id == id);
        }
        public void Update(string id, BookEntity bookIn)
        {
            _books.ReplaceOne(book => book.Id == id, bookIn);
        }
    }
}
