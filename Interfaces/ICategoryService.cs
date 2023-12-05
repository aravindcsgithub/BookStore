using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryListAsync();
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Guid id, Category category);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
