namespace AD_Coursework.DTOs.Book
{
    public class BookSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; }
        public double AverageRating { get; set; }
    }
}
