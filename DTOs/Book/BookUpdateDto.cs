using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Book
{
    public class BookUpdateDto
    {
        [Required(ErrorMessage = "Book Title is required.")]
        [StringLength(50, ErrorMessage = "Book Title cannot exceed 50 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, ErrorMessage = "ISBN cannot exceed 13 characters.")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publication Date is required.")]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "Language is required.")]
        [StringLength(50, ErrorMessage = "Language cannot exceed 50 characters.")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required.")]
        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Format is required.")]
        [StringLength(20, ErrorMessage = "Format cannot exceed 20 characters.")]
        public string Format { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }

        public bool IsAwardWinner { get; set; }

        public bool IsComingSoon { get; set; }

        [Required(ErrorMessage = "Publisher ID is required.")]
        public Guid PublisherId { get; set; }

        [Required(ErrorMessage = "Author ID is required.")]
        public Guid AuthorId { get; set; }

        public List<Guid> GenreIds { get; set; } = new List<Guid>();
    }
}
