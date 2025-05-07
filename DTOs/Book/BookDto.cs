namespace AD_Coursework.DTOs.Book
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string Format { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsOnSale { get; set; }
        public bool IsAwardWinner { get; set; }
        public bool IsComingSoon { get; set; }
        public double AverageRating { get; set; }
        public int StockQuantity { get; set; }
        public DateTime PublicationDate { get; set; }
        public List<string> Genres { get; set; } = new List<string>();

        public bool IsNewRelease => (DateTime.UtcNow - PublicationDate).TotalDays <= 90;

        public bool IsNewArrival => (DateTime.UtcNow - CreatedAt).TotalDays <= 30;

        public DateTime CreatedAt { get; set; }

        public decimal? DiscountPercentage { get; set; }
        public DateTime? DiscountEndDate { get; set; }
    }
}
