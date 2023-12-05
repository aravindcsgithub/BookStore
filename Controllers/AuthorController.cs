using BookStore.DTO;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
               _authorService = authorService;
        }
        //GET: api/author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAuthorListAsync();
            return Ok(authors);
        }

        //GET: api/author/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthorByID(Guid id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound("Invalid id");
            }
            return Ok(author);
        }

        //POST: api/author
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(CreateAuthor createAuthor)
        {
            if (createAuthor != null)
            {
                Author author = new Author()
                {
                    Name = createAuthor.AuthorName
                };
                var newAuthor = await _authorService.CreateAuthorAsync(author);
                return CreatedAtAction(nameof(GetAllAuthorByID), new { id = newAuthor.Id},newAuthor);
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }

        //PUT: api/author/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor(Guid id, UpdateAuthor updateAuthor)
        {
            if (updateAuthor != null)
            {
                if (id != updateAuthor.AuthorId)
                {
                    return BadRequest();
                }
                Author author = new Author()
                {
                    Id = updateAuthor.AuthorId,
                    Name = updateAuthor.AuthorName
                };

                var updatedAuthor = await _authorService.UpdateAuthorAsync(id, author);
                if (updatedAuthor == null)
                {
                    return NotFound();
                }
                return Ok(updatedAuthor);
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
        //DELETE: api/author/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(Guid id)
        {
            var isDeleted = await _authorService.DeleteAuthorAsync(id);
            if (!isDeleted)
            {
                return NotFound("Unable to delete record");
            }
            return Ok("Record has been deleted");
        }

    }
}
