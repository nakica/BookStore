using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.ViewModels
{
    public class BookSetViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string AuthorFirstName { get; set; }

        [Required]
        public string AuthorLastName { get; set; }
    }
}
