using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BooksApi.Domain.Entities;
using BooksApi.Infraestructure.Data.Repostory;
using AutoMapper;
using BooksApi.Domain.Dtos;

namespace BooksApi.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BaseRepository<BookEntity> _bookService;
        private readonly IMapper _mapper;
        public BooksController(BaseRepository<BookEntity> bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<BookDto>> Get() { 
           var book = _bookService.Get();
           return book == null ? NotFound(new { message = "Não encontrado" }) : _mapper.Map<List<BookDto>>(book);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public ActionResult<BookDto> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound(new { message = "Não encontrado" });
            }

            return _mapper.Map<BookDto>(book);
        }
        //[Authorize]
        [HttpPost]
        public ActionResult<BookDto> Create(BookDto book)
        {
            var createdBook =_bookService.Create(_mapper.Map<BookEntity>(book));
            return CreatedAtRoute("GetBook", new { id = createdBook.Id.ToString() }, _mapper.Map<BookEntity>(createdBook));
        }
        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(string id, BookDto bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, _mapper.Map<BookEntity>(bookIn));

            return NoContent();
        }
        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id.ToString());

            return NoContent();
        }

    }
}
