using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Book;
using Microsoft.AspNetCore.Mvc;

namespace AD_Coursework.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService, ILogger<BookController> logger)
            : base(logger)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (books, totalCount) = await _bookService.GetAllAsync(page, pageSize);
                if (!books.Any())
                {
                    return Success(books, "No books found.");
                }
                return Paginated(books, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve books");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(id);
                if(book == null)
                {
                    return Error("Book not found.", StatusCodes.Status404NotFound);
                }
                return Success(book, "Book retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve book");
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] BookCreateDto bookCreateDto)
        {
            try
            {
                if(bookCreateDto == null)
                {
                    return Error("Request body cannot be empty.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error("Invalid data. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var book = await _bookService.CreateAsync(bookCreateDto);
                return Success(book, "Book created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create book");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromForm] BookUpdateDto bookUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Invalid data", StatusCodes.Status400BadRequest, ModelState);
                }

                var book = await _bookService.UpdateAsync(id, bookUpdateDto);
                return book == null ? NotFound() : Success(book, "Book updated successfully");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update book");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _bookService.DeleteAsync(id);
                return result ? Success<Object>(null, "Book deleted successfully") : NotFound();
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to delete book");
            }
        }

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
                return HandleException(ex, "Failed to retrieve new releases");
            }
        }

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
                return HandleException(ex, "Failed to retrieve new arrivals");
            }
        }

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
                return HandleException(ex, "Failed to retrieve best sellers");
            }
        }

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
                return HandleException(ex, "Failed to retrieve award winners");
            }
        }

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
                return HandleException(ex, "Failed to retrieve coming soon books");
            }
        }

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
                return HandleException(ex, "Failed to retrieve books by author");
            }
        }

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
                return HandleException(ex, "Failed to retrieve books by publisher");
            }
        }

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
                return HandleException(ex, "Failed to retrieve books by genre");
            }
        }

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
                return HandleException(ex, "Failed to search books");
            }
        }

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
                return HandleException(ex, "Failed to filter books");
            }
        }
    }
}