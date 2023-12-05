using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.AppDbContext
{
    public class SeedData
    {
        //public static void Initialize(IServiceProvider serviceProvider)
        //{
        //    using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        //    {
        //        if (context.AppUsers.Any())
        //        {
        //            return;
        //        }

        //        context.AppUsers.AddRange(
        //            new AppUser
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "admin",
        //                Password = "admin123",
        //                Role = "admin"
        //            },
        //            new AppUser
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user",
        //                Password = "user123",
        //                Role = "user"
        //            }
        //            );
        //        context.SaveChanges();
        //    }
        //}
    }
}
