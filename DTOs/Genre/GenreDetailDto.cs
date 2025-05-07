using AD_Coursework.DTOs.Book;

namespace AD_Coursework.DTOs.Genre
{
    public class GenreDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BookCount { get; set; }
        public List<BookSummaryDto> Books { get; set; } = new List<BookSummaryDto>();
    }
}
