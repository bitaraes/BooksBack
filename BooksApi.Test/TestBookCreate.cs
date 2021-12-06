using BooksApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Test
{
    public class TestBookCreate
    {
        public BookEntity createBook()
        {
            BookEntity book = new BookEntity();
            book.BookName = Faker.Name.FullName();
            book.Category = Faker.Lorem.GetFirstWord();
            book.Price = Faker.RandomNumber.Next();
            book.Author = Faker.Name.FullName();

            return book;
        }
    }
}
