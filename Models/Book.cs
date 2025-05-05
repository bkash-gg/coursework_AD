using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required, StringLength(13)]
        public required string ISBN { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required]
        [StringLength(50)]
        public required string Language { get; set; }

        [Required]
        public required string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [StringLength(20)]
        public required string Format { get; set; }

        public string? CoverImageUrl { get; set; }

        public bool IsOnSale { get; set; }
        public bool IsAwardWinner { get; set; }
        public bool IsComingSoon { get; set; }

        [Required]
        [ForeignKey("Publisher")]
        public Guid PublisherId { get; set; }
        public required virtual Publisher Publisher { get; set; }

        [Required]
        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        public required virtual Author Author { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<WhitelistItem> WhitelistItems { get; set; } = new List<WhitelistItem>();

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
