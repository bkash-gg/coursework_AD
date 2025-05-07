using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Bio { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
