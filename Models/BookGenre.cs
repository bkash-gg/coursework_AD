using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class BookGenre
    {
        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; } = null!;

        [Required]
        [ForeignKey("Genre")]
        public Guid GenreId { get; set; }
        public virtual Genre Genre { get; set; } = null!;

    }
}