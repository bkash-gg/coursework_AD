using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD_Coursework.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [StringLength(100)]
        public required string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [StringLength(255)]
        public string? Address { get; set; }

        [Required]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<WhitelistItem> WhitelistItems { get; set; } = new List<WhitelistItem>();
    }
}
