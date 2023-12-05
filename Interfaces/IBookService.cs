using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBookListAsync();
        Task<Book> GetBookByIdAsync(Guid id);
        Task<Book> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(Guid id, Book book);
        Task<bool> DeleteBookAsync(Guid id);
        Task<string> ValidateModel(Book book);
        Task<IEnumerable<Book>> GetBookByAuthorOrCategory(string searchKey);
        Task<IEnumerable<Book>> GetBookByTitleOrCategory(string searchKey);

    }
}
