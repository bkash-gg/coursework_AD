namespace AD_Coursework.DTOs.Book
{
    public class BookDto
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
        public string? CoverImageUrl { get; set; }
        public bool IsAwardWinner { get; set; }
        public bool IsComingSoon { get; set; }
        public string PublisherName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public List<string> Genres { get; set; } = new(); 
        public DateTime CreatedAt { get; set; }
    }
}
