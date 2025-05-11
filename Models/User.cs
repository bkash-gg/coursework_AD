using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AD_Coursework.Models
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [StringLength(255)]
        public string? Address { get; set; }

        public int TotalOrdersCompleted { get; set; } = 0;

        public bool IsEligibleForLoyaltyDiscount { get; set; } = false;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        [Required]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<WhitelistItem> WhitelistItems { get; set; } = new List<WhitelistItem>();

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
