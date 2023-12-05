namespace BookStore.DTO
{
    public class UpdateBook
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string PublishedYear { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
