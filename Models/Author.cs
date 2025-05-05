using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD_Coursework.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Column(TypeName = "text")]
        public string? Bio { get; set; }  

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
