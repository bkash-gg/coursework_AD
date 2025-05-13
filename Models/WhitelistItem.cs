using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class WhitelistItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public required virtual User User { get; set; }

        [Required]
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public required virtual Book Book { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

    }
}
