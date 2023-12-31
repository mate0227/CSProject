﻿using konyvtar.Contracts;
using konyvtar.IServices;
using Microsoft.AspNetCore.Mvc;



namespace konyvtar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _bookService.Get();
            return Ok(books.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var existingBook = await _bookService.Get(book.Id);
            if (existingBook is not null)
            {
                return Conflict();
            }

            await _bookService.Add(book);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _bookService.Update(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookService.Delete(book);
            return Ok(book);
        }
    }
}
