using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Create();
        List<object> Get();
        object GetById(string id);
        void Update(string id);
        void Delete(string id);
    }
}
