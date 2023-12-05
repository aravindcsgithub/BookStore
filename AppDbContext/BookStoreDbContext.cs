using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.AppDbContext
{
    public class BookStoreDbContext: IdentityDbContext<ApplicationUser>

    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options) 
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
