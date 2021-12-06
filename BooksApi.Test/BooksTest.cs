using BooksApi.Controllers;
using BooksApi.Domain.Dtos;
using BooksApi.Domain.Entities;
using BooksApi.Infraestructure.Data.Repostory;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace BooksApi.Test
{
    public class BooksTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly BaseRepository<BookEntity> _bookRepository;
        private ServiceProvider _serviceProvider;

        public BooksTest(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.serviceProvider;
            _bookRepository = _serviceProvider.GetService<BaseRepository<BookEntity>>();
        }
        public BookEntity createBook()
        {
            BookEntity book = new BookEntity();
            book.BookName = Faker.Name.FullName();
            book.Category = Faker.Name.Prefix();
            book.Price = Faker.RandomNumber.Next();
            book.Author = Faker.Name.FullName();

            return book;
        }
        [Fact]
        public void Create_OneBook_ReturnNotNullBook()
        {
            //Arrange   
            BookEntity book = createBook();

            //act
            var insertBook = _bookRepository.Create(book);

            //Assert
            Assert.NotNull(insertBook);
        }
        [Fact]
        public void GetAllBooks_OneBook_ReturnNotNullBooks()
        {
            //Arrange   

            //act
            var book = _bookRepository.Get();

            //Assert
            Assert.NotNull(book);
        }
        [Fact]
        public void GetBook_OneBook_ReturnNotNullBook()
        {
            //Arrange   
            BookEntity book = createBook();
            //act
            var insertBook = _bookRepository.Create(book);
            var bookExists = _bookRepository.Get(insertBook.Id);

            //Assert
            Assert.NotNull(bookExists);
        }

        [Fact]
        public void Update_OneBook_ReturnUpdatedBook()
        {
            //Arrange   
            BookEntity book = createBook();
            BookEntity bookNewState = createBook();
            //act
            var insertBook = _bookRepository.Create(book);
            bookNewState.Id = insertBook.Id;
            _bookRepository.Update(insertBook.Id, bookNewState);
            var bookState = _bookRepository.Get(insertBook.Id);
            //Assert
            Assert.NotEqual(bookState.Author, book.Author);
            Assert.NotEqual(bookState.Category, book.Category);
            Assert.NotEqual(bookState.Price, book.Price);
            Assert.NotEqual(bookState.BookName, book.BookName);
        }

        [Fact]
        public void Delete_OneBook_ReturnNullBook()
        {
            //Arrange   
            BookEntity book = createBook();

            //act
            var insertBook = _bookRepository.Create(book);
            _bookRepository.Remove(insertBook.Id);
            var bookExists = _bookRepository.Get(insertBook.Id);

            //Assert
            Assert.Null(bookExists);
        }

    }
}
