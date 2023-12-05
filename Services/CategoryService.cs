using BookStore.AppDbContext;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly BookStoreDbContext _dbContext;
        public CategoryService(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            _dbContext.Category.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            Category dataInDb = await _dbContext.Category.FirstOrDefaultAsync(a => a.Id == id);
            if (dataInDb != null)
            {
                _dbContext.Category.Remove(dataInDb);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            Category dataInDb = await _dbContext.Category.FirstOrDefaultAsync(a => a.Id == id);
            return dataInDb;
        }

        public async Task<IEnumerable<Category>> GetCategoryListAsync()
        {
            return await _dbContext.Category.ToListAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Guid id, Category category)
        {
            Category dataInDb = await _dbContext.Category.FirstOrDefaultAsync(a => a.Id == id);
            if (dataInDb != null)
            {
                dataInDb.Name = category.Name;
                await _dbContext.SaveChangesAsync();
            }
            return category;
        }
    }
}
