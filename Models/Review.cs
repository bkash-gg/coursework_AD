using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public virtual User User { get; set; } = null!;

        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
