namespace BookStore.Web.ViewModels
{
    using BookStore.Data.Models;
    using System.Collections.Generic;

    public class BookDetailsViewModel
    {
        public IEnumerable<Book> BookDetails { get; set; }
    }
}
