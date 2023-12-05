using BookStore.AppDbContext;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BookStoreDbContext _dbContext;
        public AuthorService(BookStoreDbContext dbContext)
        {
                _dbContext=dbContext;
        }
        public async Task<Author> CreateAuthorAsync(Author author)
        {
            author.Id = Guid.NewGuid();
            _dbContext.Author.Add(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAuthorAsync(Guid id)
        {
            Author dataInDb = await _dbContext.Author.FirstOrDefaultAsync(a => a.Id == id);
            if (dataInDb != null)
            {
                _dbContext.Author.Remove(dataInDb);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            Author dataInDb = await _dbContext.Author.FirstOrDefaultAsync(a => a.Id == id);
            return dataInDb;
        }

        public async Task<IEnumerable<Author>> GetAuthorListAsync()
        {
            return await _dbContext.Author.ToListAsync();
        }

        public async Task<Author> UpdateAuthorAsync(Guid id, Author author)
        {
            Author dataInDb = await _dbContext.Author.FirstOrDefaultAsync(a => a.Id == id);
            if(dataInDb != null)
            {
                dataInDb.Name=author.Name;
                await _dbContext.SaveChangesAsync();
            }
            return author;
        }
    }
}
