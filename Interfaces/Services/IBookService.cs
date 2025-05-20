using AD_Coursework.DTOs.Book;

namespace AD_Coursework.Interfaces.Services
{
    public interface IBookService
    {
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetAllAsync(int page, int pageSize);
        Task<BookDto?> GetByIdAsync(Guid id, Guid userId);
        Task<bool> CreateAsync(BookCreateDto bookCreateDto);
        Task<bool> UpdateAsync(Guid id, BookUpdateDto bookUpdateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetNewReleasesAsync(int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetNewArrivalsAsync(int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBestSellersAsync(int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetAwardWinnersAsync(int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetComingSoonBooksAsync(int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksByAuthorAsync(Guid authorId, int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksByPublisherAsync(Guid publisherId, int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksByGenreAsync(Guid genreId, int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> SearchBooksAsync(string searchTerm, int page, int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> FilterBooksAsync(
            string? searchTerm,
            List<Guid>? authorIds,
            List<Guid>? genreIds,
            List<Guid>? publisherIds,
            decimal? minPrice,
            decimal? maxPrice,
            string? language,
            string? format,
            bool? isAvailable,
            bool? isAwardWinner,
            bool? isComingSoon,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize);
        Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksWithDiscountsAsync(int page, int pageSize);
    }
}
