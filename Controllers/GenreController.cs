using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Genre;
using Microsoft.AspNetCore.Mvc;

namespace AD_Coursework.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        // Constructor that initializes the genre service and logger
        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
            : base(logger)
        {
            _genreService = genreService;
        }

        // Retrieves all genres from the database
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var genres = await _genreService.GetAllAsync();
                if (!genres.Any())
                {
                    return Success(genres, "No genres found.");
                }

                return Success(genres, "Genres retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching genres.");
                return HandleException(ex, "Unable to retrieve genres. Please try again later.");
            }
        }

        // Retrieves a specific genre by its unique identifier
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var genre = await _genreService.GetByIdAsync(id);
                if (genre == null)
                {
                    return Error("Genre not found.", StatusCodes.Status404NotFound);
                }

                return Success(genre, "Genre retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching genre.");
                return HandleException(ex, "Unable to retrieve genre. Please try again later.");
            }
        }

        // Creates a new genre record (admin only)
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto genreCreateDto)
        {
            try
            {
                if (genreCreateDto == null)
                {
                    return Error("Please provide genre details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error(
                        "Invalid data. Please check your details.",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }

                var genre = await _genreService.CreateAsync(genreCreateDto);
                return Success(
                    genre,
                    "Genre created successfully.",
                    StatusCodes.Status201Created
                );
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error occurred while creating genre.");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating genre.");
                return HandleException(ex, "Unable to create genre. Please try again.");
            }
        }

        // Updates an existing genre's information (admin only)
        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GenreUpdateDto genreUpdateDto)
        {
            try
            {
                if (genreUpdateDto == null)
                {
                    return Error("Please provide updated genre details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error(
                        "Invalid data. Please check your details.",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }

                var genre = await _genreService.UpdateAsync(id, genreUpdateDto);
                if (genre == null)
                {
                    return Error("Genre not found.", StatusCodes.Status404NotFound);
                }

                return Success(genre, "Genre updated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error occurred while updating genre.");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating genre.");
                return HandleException(ex, "Unable to update genre. Please try again.");
            }
        }

        // Permanently removes a genre from the system (admin only)
        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _genreService.DeleteAsync(id);
                if (!result)
                {
                    return Error("Genre not found.", StatusCodes.Status404NotFound);
                }

                return Success<object>(null, "Genre deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting genre.");
                return HandleException(ex, "Unable to delete genre. Please try again.");
            }
        }
    }
}
