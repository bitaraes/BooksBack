using AutoMapper;
using BooksApi.Domain.Entities;
using BooksApi.Domain.Repository;
using BooksApi.Infraestructure.Data.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Infraestructure.Data.Repostory
{
    
    public class UserRepository<T> : IRepository<UserEntity> where T : BaseEntity
    {
        public IMongoCollection<UserEntity> _users;
        private readonly IMapper _mapper;
        public UserRepository(IBookstoreDatabaseSettings settings, IMapper mapper)
        {
            _mapper = mapper;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserEntity>(settings.BooksCollectionName);
        }
        public UserEntity Create(UserEntity item)
        {
            throw new NotImplementedException();
        }

        public List<UserEntity> Get()
        {
            throw new NotImplementedException();
        }

        public UserEntity Get(string username)
        {
            var user = _users.Find<UserEntity>(user => user.UserName == username).FirstOrDefault();
            return _mapper.Map<UserEntity>(user);
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, UserEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
