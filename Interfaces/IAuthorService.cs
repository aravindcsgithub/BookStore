using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorListAsync();
        Task<Author> GetAuthorByIdAsync(Guid id);
        Task<Author> CreateAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(Guid id, Author author);
        Task<bool> DeleteAuthorAsync(Guid id);
    }
}
