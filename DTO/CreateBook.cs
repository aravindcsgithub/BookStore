namespace BookStore.DTO
{
    public class CreateBook
    {
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string PublishedYear { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
