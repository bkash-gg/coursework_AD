using AD_Coursework.Models;
using System.Linq.Expressions;

namespace AD_Coursework.Interfaces.Repositories
{
    public interface IBookRepository
    {
        // Basic CRUD operations
        Task<IEnumerable<Book>> GetAllBooksAsync(int page, int pageSize);
        Task<Book?> GetBookByIdAsync(Guid id);
        Task<Book> CreateBookAsync(Book book);
        Task<Book?> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(Guid id);

        // Additional query methods
        Task<int> GetTotalBooksCountAsync();
        Task<bool> BookExistsAsync(Guid id);
        Task<bool> ISBNExistsAsync(string isbn);
        Task<bool> ISBNExistsAsync(string isbn, Guid excludeId);

        // Filtering methods
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, int page, int pageSize);
        Task<IEnumerable<Book>> GetBooksByPublisherAsync(Guid publisherId, int page, int pageSize);
        Task<IEnumerable<Book>> GetBooksByGenreAsync(Guid genreId, int page, int pageSize);
        Task<IEnumerable<Book>> GetNewReleasesAsync(int page, int pageSize);
        Task<IEnumerable<Book>> GetNewArrivalsAsync(int page, int pageSize);
        Task<IEnumerable<Book>> GetBestSellersAsync(int page, int pageSize);
        Task<IEnumerable<Book>> GetAwardWinnersAsync(int page, int pageSize);
        Task<IEnumerable<Book>> GetComingSoonBooksAsync(int page, int pageSize);

        // Search and advanced filtering
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, int page, int pageSize);
        Task<IEnumerable<Book>> FilterBooksAsync(
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
        Task<int> GetFilteredBooksCountAsync(
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
            bool? isComingSoon);

        // BookGenre operations
        Task AddBookGenreAsync(BookGenre bookGenre);
        Task RemoveBookGenreAsync(Guid bookId, Guid genreId);
        Task<IEnumerable<BookGenre>> GetBookGenresByBookIdAsync(Guid bookId);
        Task<bool> BookGenreExistsAsync(Guid bookId, Guid genreId);
        Task UpdateBookGenresAsync(Guid bookId, List<Guid> genreIds);
        Task<IEnumerable<Book>> GetBooksWithDiscountsAsync(int page, int pageSize);
    }
}
