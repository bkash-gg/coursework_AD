using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD_Coursework.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public required virtual User User { get; set; }

        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public required virtual Book Book { get; set; }
    }
}
