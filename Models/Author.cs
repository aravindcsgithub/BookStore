using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
