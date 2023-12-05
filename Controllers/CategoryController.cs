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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        //GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategorys()
        {
            var categorys = await _categoryService.GetCategoryListAsync();
            return Ok(categorys);
        }

        //GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoryByID(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Invalid id");
            }
            return Ok(category);
        }

        //POST: api/category
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CreateCategory createCategory)
        {
            if (createCategory != null)
            {
                Category category = new Category()
                {
                    Name = createCategory.BookCategoryName
                };
                var newCategory = await _categoryService.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetAllCategoryByID), new { id = newCategory.Id }, newCategory);
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }

        //PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(Guid id, UpdateCategory updateCategory)
        {
            if (updateCategory != null)
            {
                if (id != updateCategory.CategoryId)
                {
                    return BadRequest();
                }
                Category category = new Category()
                {
                    Id = updateCategory.CategoryId,
                    Name = updateCategory.BookCategoryName
                };

                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, category);
                if (updatedCategory == null)
                {
                    return NotFound();
                }
                return Ok(updatedCategory);
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
        //DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(id);
            if (!isDeleted)
            {
                return NotFound("Unable to delete record");
            }
            return Ok("Record has been deleted");
        }
    }
}
