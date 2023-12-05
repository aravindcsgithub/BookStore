using BookStore.AppDbContext;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookService: IBookService
    {
        private readonly BookStoreDbContext _dbContext;
        public BookService(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            book.Id = Guid.NewGuid();
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            Book dataInDb = await _dbContext.Books.FirstOrDefaultAsync(a => a.Id == id);
            if (dataInDb != null)
            {
                _dbContext.Books.Remove(dataInDb);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Book>> GetBookByAuthorOrCategory(string searchKey)
        {
            return await _dbContext.Books
                .Include(a => a.Author)
                .Include(c=>c.Category)
                .Where(a=> a.Category.Name.Contains(searchKey) || a.Author.Name.Contains(searchKey)).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            Book dataInDb = await _dbContext.Books.FirstOrDefaultAsync(a => a.Id == id);
            return dataInDb;
        }

        public async Task<IEnumerable<Book>> GetBookByTitleOrCategory(string searchKey)
        {
            return await _dbContext.Books
                .Include(a => a.Author)
                .Include(c => c.Category)
                .Where(a => a.Category.Name.Contains(searchKey) || a.Title.Contains(searchKey)).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookListAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> UpdateBookAsync(Guid id, Book book)
        {
            Book dataInDb = await _dbContext.Books.FirstOrDefaultAsync(a => a.Id == id);
            if (dataInDb != null)
            {
                dataInDb.Title = book.Title;
                dataInDb.Desctiption=book.Desctiption;
                dataInDb.Year = book.Year;
                dataInDb.AuthorId = book.AuthorId;
                dataInDb.CategoryId = book.CategoryId;
                await _dbContext.SaveChangesAsync();
            }
            return book;
        }

        public async Task<string> ValidateModel(Book book)
        {
            string validationResult = string.Empty;
            if(book != null)
            {
                if (string.IsNullOrEmpty(book.Title))
                {
                    validationResult = "Book title is requied. ";
                }

                if (book.AuthorId !=null)
                {
                    Author checkAuthor = await _dbContext.Author.FirstOrDefaultAsync(a => a.Id == book.AuthorId);
                    if(checkAuthor == null) {
                        validationResult += "Invalid author. ";
                    }
                }
                else
                {
                    validationResult += "Author is required. ";
                }

                if (book.CategoryId != null)
                {
                    Category checkCategory = await _dbContext.Category.FirstOrDefaultAsync(a => a.Id == book.CategoryId);
                    if (checkCategory == null)
                    {
                        validationResult += "Invalid category. ";
                    }
                }
                else
                {
                    validationResult += "Category is required. ";
                }
            }
            return validationResult;
        }
    }
}
