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

        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
            : base(logger)
        {
            _authorService = authorService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var authors = await _authorService.GetAllAsync();
                if (!authors.Any())
                {
                    return Success(authors, "No authors found.");
                }

                return Success(authors, "Authors retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve authors.");
            }
        }

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

                return Success(author, "Author retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve author.");
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto authorCreateDto)
        {
            try
            {
                if (authorCreateDto == null)
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

                var author = await _authorService.CreateAsync(authorCreateDto);
                return Success(
                    author,
                    "Author created successfully!",
                    StatusCodes.Status201Created
                );
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create author.");
            }
        }

        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AuthorUpdateDto authorUpdateDto)
        {
            try
            {
                if (authorUpdateDto == null)
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

                var author = await _authorService.UpdateAsync(id, authorUpdateDto);
                if (author == null)
                {
                    return Error("Author not found.", StatusCodes.Status404NotFound);
                }

                return Success(author, "Author updated successfully!");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update author.");
            }
        }

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

                return Success<object>(null, "Author deleted successfully!");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to delete author.");
            }
        }
    }
}