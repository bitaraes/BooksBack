using BooksApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Domain.Interfaces.Services
{
    public interface IBooksService
    {
        public List<BookDto> Get();
        public BookDto Get(string id);
        public BookDto Create(BookDto book);
        public Task Update(string id, BookDto bookIn);
        public Task Remove(string id);
    }
}
