using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Author;
using Microsoft.AspNetCore.Mvc;

namespace AD_Coursework.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        // Constructor that initializes the author service and logger
        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
            : base(logger)
        {
            _authorService = authorService;
        }

        // Retrieves all authors from the database
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var authors = await _authorService.GetAllAsync();
                if (!authors.Any())
                {
                    return Success(authors, "No authors available.");
                }

                return Success(authors, "Authors fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching authors.");
                return HandleException(ex, "Unable to fetch authors. Please try again later.");
            }
        }

        // Retrieves a specific author by their unique identifier
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var author = await _authorService.GetByIdAsync(id);
                if (author == null)
                {
                    return Error("Author not found.", StatusCodes.Status404NotFound);
                }

                return Success(author, "Author fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching author.");
                return HandleException(ex, "Unable to fetch author. Please try again later.");
            }
        }

        // Creates a new author record (admin only)
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto authorCreateDto)
        {
            try
            {
                if (authorCreateDto == null)
                {
                    return Error("Please provide author details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error(
                        "Invalid details. Please check your inputs.",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }

                var author = await _authorService.CreateAsync(authorCreateDto);
                return Success(
                    author,
                    "Author added successfully.",
                    StatusCodes.Status201Created
                );
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error occurred while creating author.");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating author.");
                return HandleException(ex, "Unable to create author. Please try again.");
            }
        }

        // Updates an existing author's information (admin only)
        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AuthorUpdateDto authorUpdateDto)
        {
            try
            {
                if (authorUpdateDto == null)
                {
                    return Error("Please provide updated author details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error(
                        "Invalid data. Please check your details.",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }

                var author = await _authorService.UpdateAsync(id, authorUpdateDto);
                if (author == null)
                {
                    return Error("Author not found.", StatusCodes.Status404NotFound);
                }

                return Success(author, "Author details updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating author.");
                return HandleException(ex, "Unable to update author. Please try again later.");
            }
        }

        // Permanently removes an author from the system (admin only)
        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _authorService.DeleteAsync(id);
                if (!result)
                {
                    return Error("Author not found.", StatusCodes.Status404NotFound);
                }

                return Success<object>(null, "Author deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting author.");
                return HandleException(ex, "Unable to delete author. Please try again later.");
            }
        }
    }
}
