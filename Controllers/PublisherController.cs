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

        public PublisherController(IPublisherService publisherService, ILogger<PublisherController> logger)
            : base(logger)
        {
            _publisherService = publisherService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var publishers = await _publisherService.GetAllAsync();
                if (!publishers.Any())
                {
                    return Success(publishers, "No publishers found."); 
                }

                return Success(publishers, "Publishers retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve publishers.");
            }
        }

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

                return Success(publisher, "Publisher retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to retrieve publisher.");
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PublisherCreateDto publisherCreateDto)
        {
            try
            {
                if (publisherCreateDto == null)
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

                var publisher = await _publisherService.CreateAsync(publisherCreateDto);
                return Success(
                    publisher,
                    "Publisher created successfully!",
                    StatusCodes.Status201Created
                );
            }
            catch (InvalidOperationException ex)
            {
                return Error(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create publisher.");
            }
        }

        [HttpPut("{id}/update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PublisherUpdateDto publisherUpdateDto)
        {
            try
            {
                if (publisherUpdateDto == null)
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

                var publisher = await _publisherService.UpdateAsync(id, publisherUpdateDto);
                if (publisher == null)
                {
                    return Error("Publisher not found.", StatusCodes.Status404NotFound);
                }

                return Success(publisher, "Publisher updated successfully!");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to update publisher.");
            }
        }

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

                return Success<object>(null, "Publisher deleted successfully!");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to delete publisher.");
            }
        }
    }
}
