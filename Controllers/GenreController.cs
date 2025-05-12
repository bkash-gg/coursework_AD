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

        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
            : base(logger)
        {
            _genreService = genreService;
        }

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
                return HandleException(ex, "Failed to retrieve genres.");
            }
        }

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
                return HandleException(ex, "Failed to retrieve genre.");
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto genreCreateDto)
        {
            try
            {
                if (genreCreateDto == null)
                {
                    return Error("Request body cannot be empty.", StatusCodes.Status400BadRequest);
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
                    "Genre created successfully!",
                    StatusCodes.Status201Created
                );
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create genre.");
            }
        }

        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GenreUpdateDto genreUpdateDto)
        {
            try
            {
                if (genreUpdateDto == null)
                {
                    return Error("Request body cannot be empty.", StatusCodes.Status400BadRequest);
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

                return Success(genre, "Genre updated successfully!");
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update genre.");
            }
        }

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

                return Success<object>(null, "Genre deleted successfully!");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to delete genre.");
            }
        }
    }
}