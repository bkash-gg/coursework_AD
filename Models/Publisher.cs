using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Publisher
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

        [Required]
        [StringLength(100)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
