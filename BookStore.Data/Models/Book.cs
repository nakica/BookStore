namespace BookStore.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Genre { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
