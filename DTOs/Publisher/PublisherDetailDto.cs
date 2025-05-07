using AD_Coursework.DTOs.Book;

namespace AD_Coursework.DTOs.Publisher
{
    public class PublisherDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<BookSummaryDto> Books { get; set; } = new List<BookSummaryDto>();
    }
}
