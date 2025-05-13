using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using AD_Coursework.DTOs.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace AD_Coursework.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : BaseController
    {
        private readonly IPublisherService _publisherService;

        // Constructor that initializes the publisher service and logger
        public PublisherController(IPublisherService publisherService, ILogger<PublisherController> logger)
            : base(logger)
        {
            _publisherService = publisherService;
        }

        // Retrieves all publishers from the database
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var publishers = await _publisherService.GetAllAsync();
                if (!publishers.Any())
                {
                    return Success(publishers, "No publishers available.");
                }

                return Success(publishers, "Publishers fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching publishers.");
                return HandleException(ex, "Unable to fetch publishers. Please try again later.");
            }
        }

        // Retrieves a specific publisher by their unique identifier
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var publisher = await _publisherService.GetByIdAsync(id);
                if (publisher == null)
                {
                    return Error("Publisher not found.", StatusCodes.Status404NotFound);
                }

                return Success(publisher, "Publisher fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching publisher.");
                return HandleException(ex, "Unable to fetch publisher. Please try again later.");
            }
        }

        // Creates a new publisher record (admin only)
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PublisherCreateDto publisherCreateDto)
        {
            try
            {
                if (publisherCreateDto == null)
                {
                    return Error("Please provide publisher details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error(
                        "Invalid details. Please check your inputs.",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }

                var publisher = await _publisherService.CreateAsync(publisherCreateDto);
                return Success(
                    publisher,
                    "Publisher added successfully.",
                    StatusCodes.Status201Created
                );
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error occurred while creating publisher.");
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating publisher.");
                return HandleException(ex, "Unable to create publisher. Please try again.");
            }
        }

        // Updates an existing publisher's information (admin only)
        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PublisherUpdateDto publisherUpdateDto)
        {
            try
            {
                if (publisherUpdateDto == null)
                {
                    return Error("Please provide updated publisher details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error(
                        "Invalid data. Please check your details.",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }

                var publisher = await _publisherService.UpdateAsync(id, publisherUpdateDto);
                if (publisher == null)
                {
                    return Error("Publisher not found.", StatusCodes.Status404NotFound);
                }

                return Success(publisher, "Publisher details updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating publisher.");
                return HandleException(ex, "Unable to update publisher. Please try again later.");
            }
        }

        // Permanently removes a publisher from the system (admin only)
        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _publisherService.DeleteAsync(id);
                if (!result)
                {
                    return Error("Publisher not found.", StatusCodes.Status404NotFound);
                }

                return Success<object>(null, "Publisher deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting publisher.");
                return HandleException(ex, "Unable to delete publisher. Please try again later.");
            }
        }
    }
}