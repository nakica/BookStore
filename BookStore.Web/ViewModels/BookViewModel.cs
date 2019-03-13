namespace BookStore.Web.ViewModels
{
    using BookStore.Data.Models;

    public class BookViewModel
    {
        public Book BookDetails { get; set; }

        public Author AuthorFirstName { get; set; }

        public Author AuthorLastName { get; set; }
    }
}
