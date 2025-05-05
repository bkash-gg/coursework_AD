using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Publisher
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(15)]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
