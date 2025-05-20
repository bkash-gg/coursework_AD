using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Announcement : IValidatableObject
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        [Required]
        public string Type { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "EndDate must be later than StartDate.",
                    new[] { nameof(EndDate) });
            }
        }
    }
}
