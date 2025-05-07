using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Book
{
    public class BookUpdateDto
    {
        [Required]
        [StringLength(100)]
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
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be 0 or greater")]
        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [StringLength(20)]
        public string Format { get; set; } = string.Empty;

        public string CoverImageUrl { get; set; } = string.Empty;

        public bool IsOnSale { get; set; }

        public bool IsAwardWinner { get; set; }

        public bool IsComingSoon { get; set; }

        [Required]
        public Guid PublisherId { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public List<Guid> GenreIds { get; set; } = new List<Guid>();
    }
}
