using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data;
using SimpleWebApi.Entities;

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext context;

        public BooksController(BookContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> List()
        {
            var books = context.Books;
            return books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.BookId.Equals(id));
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            context.Entry(book).State = EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.BookId.Equals(id));

            if (book == null)
            {
                return NotFound();
            }

            context.Books.Remove(book);
            context.SaveChanges();

            return NoContent();
        }
    }
}