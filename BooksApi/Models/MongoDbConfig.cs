using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Models
{
    public class MongoDbConfig
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
