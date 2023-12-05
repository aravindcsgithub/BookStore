using BookStore.DTO;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        //GET: api/book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetBookListAsync();
            return Ok(books);
        }

        //GET: api/book/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBookByID(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Invalid id");
            }
            return Ok(book);
        }

        //POST: api/book
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateBook createBook)
        {
            if (createBook != null)
            {
                Book book = new Book()
                {
                    Title = createBook.BookTitle,
                    Desctiption = createBook.BookDescription,
                    Year = createBook.PublishedYear,
                    AuthorId = createBook.AuthorId,
                    CategoryId = createBook.CategoryId
                };
                string validationText = await _bookService.ValidateModel(book);
                if (string.IsNullOrEmpty(validationText))
                {
                    var newBook = await _bookService.CreateBookAsync(book);
                    return CreatedAtAction(nameof(GetAllBookByID), new { id = newBook.Id }, newBook);
                }
                else
                {
                    return BadRequest(validationText);
                }
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }

        //PUT: api/book/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(Guid id, UpdateBook updateBook)
        {
            if (updateBook != null)
            {
                if (id != updateBook.BookId)
                {
                    return BadRequest();
                }
                Book book = new Book()
                {
                    Id = updateBook.BookId,
                    Title = updateBook.BookTitle,
                    Desctiption = updateBook.BookDescription,
                    Year = updateBook.PublishedYear,
                    AuthorId = updateBook.AuthorId,
                    CategoryId = updateBook.CategoryId
                };
                string validationText = await _bookService.ValidateModel(book);
                if (string.IsNullOrEmpty(validationText))
                {
                    var updatedBook = await _bookService.UpdateBookAsync(id, book);
                    if (updatedBook == null)
                    {
                        return NotFound();
                    }
                    return Ok(updatedBook);
                }
                else
                {
                    return BadRequest(validationText);
                }
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
        //DELETE: api/book/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            var isDeleted = await _bookService.DeleteBookAsync(id);
            if (!isDeleted)
            {
                return NotFound("Unable to delete record");
            }
            return Ok("Record has been deleted");
        }

        
    }
}
