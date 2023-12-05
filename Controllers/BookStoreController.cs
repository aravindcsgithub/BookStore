using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStore.Controllers
{
    [Route("api/bookstore")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class BookStoreController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookStoreController(IBookService bookService)
        {
            _bookService = bookService;
        }
        //GET: api/bookstore/{BookStore}
        [HttpGet("search-by/author-category/{search}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByAuthorOrCategory(string search)
        {
            var books = await _bookService.GetBookByAuthorOrCategory(search);
            if (books == null || books.Count() == 0)
            {
                return NotFound("No matches found for " + search);
            }
            return Ok(books);
        }
        //GET: api/bookstore/{BookStore}
        [HttpGet("search-by/title-category/{search}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByTitleOrCategory(string search)
        {
            var books = await _bookService.GetBookByTitleOrCategory(search);
            if (books == null || books.Count() == 0)
            {
                return NotFound("No matches found for " + search);
            }
            return Ok(books);
        }
    }
}
