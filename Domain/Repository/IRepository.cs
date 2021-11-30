using BooksApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Domain.Repository
{
    public interface IRepository<T> where T:BaseEntity
    {
        public List<T> Get();
        public T Get(string id);
        public T Create(T item);
        public void Update(string id, T item);
        public void Remove(string id);
    }
}
