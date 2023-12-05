using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
