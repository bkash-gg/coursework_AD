using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.BookGenre
{
    public class BookGenreCreateDto
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "Genre ID is required.")]
        public Guid GenreId { get; set; }
    }
}
