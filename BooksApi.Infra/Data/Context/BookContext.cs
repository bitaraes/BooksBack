using BooksApi.Domain.Entities;
using BooksApi.Infraestructure.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Infraestructure.Data.Context
{
    public class BookContext
    {
        public IMongoCollection<BookEntity> _books;

        public BookContext(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<BookEntity>(settings.BooksCollectionName);
        }
    }
}
