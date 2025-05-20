using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using AD_Coursework.DTOs.Book;
using AD_Coursework.Extensions;

namespace AD_Coursework.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        // Constructor that initializes the book service and logger
        public BookController(IBookService bookService, ILogger<BookController> logger)
            : base(logger)
        {
            _bookService = bookService;
        }

        // Retrieves all books with pagination support
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetAllAsync(page, pageSize);
                if (!books.Any())
                {
                    return Success(books, "No books found in our collection.");
                }
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all books.");
                return HandleException(ex, "We couldn't retrieve the book list. Please try again later.");
            }
        }

        // Retrieves a specific book by its unique identifier
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var userId = User.GetUserId();
                var book = await _bookService.GetByIdAsync(id, userId);
                if (book == null)
                {
                    return Error("We couldn't find the book you're looking for.", StatusCodes.Status404NotFound);
                }
                return Success(book, "Book details retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving book with ID: {id}");
                return HandleException(ex, "We couldn't retrieve the book details. Please try again.");
            }
        }

        // Creates a new book (Admin only)
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] BookCreateDto bookCreateDto)
        {
            try
            {
                if (bookCreateDto == null)
                {
                    return Error("Please provide the book details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error("Invalid book information. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var book = await _bookService.CreateAsync(bookCreateDto);
                return Success<Object>(null, "The book has been added successfully.", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new book.");
                return HandleException(ex, "We couldn't add the new book. Please try again later.");
            }
        }

        // Updates an existing book's details (Admin only)
        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromForm] BookUpdateDto bookUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid book information. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var book = await _bookService.UpdateAsync(id, bookUpdateDto);
                if (!book)
                {
                    return Error("We couldn't find the book to update.", StatusCodes.Status404NotFound);
                }

                return Success<Object>(null, "The book details have been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating book with ID: {id}");
                return HandleException(ex, "We couldn't update the book details. Please try again.");
            }
        }

        // Deletes a book from the system (Admin only)
        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _bookService.DeleteAsync(id);
                return result ? Success<Object>(null, "The book has been removed successfully.") : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting book with ID: {id}");
                return HandleException(ex, "We couldn't remove the book. Please try again.");
            }
        }

        // Retrieves newly released books with pagination support
        [HttpGet("new-releases")]
        public async Task<IActionResult> GetNewReleases([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetNewReleasesAsync(page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving new releases.");
                return HandleException(ex, "We couldn't retrieve the new releases. Please try again later.");
            }
        }

        // Retrieves newly arrived books with pagination support
        [HttpGet("new-arrivals")]
        public async Task<IActionResult> GetNewArrivals([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetNewArrivalsAsync(page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving new arrivals.");
                return HandleException(ex, "We couldn't retrieve the new arrivals. Please try again later.");
            }
        }

        // Retrieves best-selling books with pagination support
        [HttpGet("best-sellers")]
        public async Task<IActionResult> GetBestSellers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetBestSellersAsync(page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving best sellers.");
                return HandleException(ex, "We couldn't retrieve the best sellers. Please try again later.");
            }
        }

        // Retrieves award-winning books with pagination support
        [HttpGet("award-winners")]
        public async Task<IActionResult> GetAwardWinners([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetAwardWinnersAsync(page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving award winners.");
                return HandleException(ex, "We couldn't retrieve the award-winning books. Please try again later.");
            }
        }

        // Retrieves upcoming books with pagination support
        [HttpGet("coming-soon")]
        public async Task<IActionResult> GetComingSoonBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetComingSoonBooksAsync(page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving upcoming books.");
                return HandleException(ex, "We couldn't retrieve the upcoming books. Please try again later.");
            }
        }

        // Retrieves books by a specific author with pagination support
        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(Guid authorId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetBooksByAuthorAsync(authorId, page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving books by author with ID: {authorId}");
                return HandleException(ex, "We couldn't retrieve books by this author. Please try again later.");
            }
        }

        // Retrieves books by a specific publisher with pagination support
        [HttpGet("publisher/{publisherId}")]
        public async Task<IActionResult> GetBooksByPublisher(Guid publisherId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetBooksByPublisherAsync(publisherId, page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving books by publisher with ID: {publisherId}");
                return HandleException(ex, "We couldn't retrieve books by this publisher. Please try again later.");
            }
        }

        // Retrieves books by a specific genre with pagination support
        [HttpGet("genre/{genreId}")]
        public async Task<IActionResult> GetBooksByGenre(Guid genreId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetBooksByGenreAsync(genreId, page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving books by genre with ID: {genreId}");
                return HandleException(ex, "We couldn't retrieve books in this genre. Please try again later.");
            }
        }

        // Searches books by a search term with pagination support
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.SearchBooksAsync(searchTerm, page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while searching books with term: {searchTerm}");
                return HandleException(ex, "We couldn't complete your search. Please try again later.");
            }
        }

        // Retrieves books with discounts with pagination support
        [HttpGet("deals")]
        public async Task<IActionResult> GetBooksWithDiscounts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetBooksWithDiscountsAsync(page, pageSize);
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving books with discounts.");
                return HandleException(ex, "We couldn't retrieve discounted books. Please try again later.");
            }
        }

        // Filters books based on multiple criteria with pagination and sorting support
        [HttpGet("filter")]
        public async Task<IActionResult> FilterBooks(
            [FromQuery] string? searchTerm,
            [FromQuery] List<Guid>? authorIds,
            [FromQuery] List<Guid>? genreIds,
            [FromQuery] List<Guid>? publisherIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? language,
            [FromQuery] string? format,
            [FromQuery] bool? isAvailable,
            [FromQuery] bool? isAwardWinner,
            [FromQuery] bool? isComingSoon,
            [FromQuery] string? sortBy = "title",
            [FromQuery] bool ascending = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.FilterBooksAsync(
                    searchTerm,
                    authorIds,
                    genreIds,
                    publisherIds,
                    minPrice,
                    maxPrice,
                    language,
                    format,
                    isAvailable,
                    isAwardWinner,
                    isComingSoon,
                    sortBy,
                    ascending,
                    page,
                    pageSize);

                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while filtering books.");
                return HandleException(ex, "We couldn't filter the books. Please try again later.");
            }
        }
    }
}