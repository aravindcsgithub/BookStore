using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Desctiption { get; set; }
        [StringLength(4)]
        public string? Year { get; set; }

        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
