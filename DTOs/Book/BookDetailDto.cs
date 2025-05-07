using AD_Coursework.DTOs.Author;
using AD_Coursework.DTOs.Discount;
using AD_Coursework.DTOs.Genre;
using AD_Coursework.DTOs.Publisher;
using AD_Coursework.DTOs.Review;

namespace AD_Coursework.DTOs.Book
{
    public class BookDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public string Format { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsOnSale { get; set; }
        public bool IsAwardWinner { get; set; }
        public bool IsComingSoon { get; set; }

        public AuthorDto Author { get; set; } = new AuthorDto();

        public PublisherDto Publisher { get; set; } = new PublisherDto();

        public List<GenreDto> Genres { get; set; } = new List<GenreDto>();

        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

        public double AverageRating { get; set; }

        public DiscountDto ActiveDiscount { get; set; } = new DiscountDto();

        public DateTime CreatedAt { get; set; }
    }
}
