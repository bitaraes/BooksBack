using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BooksApi.Infraestructure.Data.Repostory;
using BooksApi.Domain.Entities;

namespace BooksApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BaseRepository<BookEntity> _bookService;

        [HttpGet]
        public ActionResult<List<BookEntity>> Get()
        {
            var book = _bookService.Get();
            return book == null ? NotFound(new { message = "Não encontrado" }) : book;
        }

        [HttpGet("{id}", Name = "GetBook")]
        public ActionResult<BookEntity> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound(new { message = "Não encontrado" });
            }

            return book;
            //return View("index", book);
        }
        [Authorize]
        [HttpPost]
        public ActionResult<BookEntity> Create(BookEntity book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(string id, BookEntity bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }

    }
}
