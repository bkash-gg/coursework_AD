using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD_Coursework.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = null!;

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}