using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD_Coursework.Models
{
    public class Announcement
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public required string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public required string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public bool IsActive =>
            StartDate <= DateTime.UtcNow &&
            (!EndDate.HasValue || EndDate >= DateTime.UtcNow);

        [Required]
        [StringLength(50)]
        public required string Type { get; set; }
    }
}
