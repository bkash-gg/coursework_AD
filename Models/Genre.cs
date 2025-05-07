using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}