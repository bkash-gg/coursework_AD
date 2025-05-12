using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Language { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [StringLength(20)]
        public string Format { get; set; } = string.Empty;

        public string? CoverImageUrl { get; set; }

        public bool IsAwardWinner { get; set; }
        public bool IsComingSoon { get; set; }

        [Required]
        [ForeignKey("Publisher")]
        public Guid PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; } = null!;

        [Required]
        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<WhitelistItem> WhitelistItems { get; set; } = new List<WhitelistItem>();

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
