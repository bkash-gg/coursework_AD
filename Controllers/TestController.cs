using AD_Coursework.Interfaces.Services;
using AD_Coursework.Models;
using Microsoft.AspNetCore.Mvc;

namespace AD_Coursework.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public TestController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("email")]
        public async Task<IActionResult> TestEmail()
        {
            try
            {
                var testOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTime.UtcNow,
                    ClaimCode = "TEST123",
                    Subtotal = 100,
                    DiscountAmount = 10,
                    DiscountPercentage = 10,
                    TotalAmount = 90,
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Book = new Book { Title = "Sample Book 1" },
                        Quantity = 1,
                        UnitPrice = 50,
                        LineTotal = 50
                    },
                    new OrderItem
                    {
                        Book = new Book { Title = "Sample Book 2" },
                        Quantity = 2,
                        UnitPrice = 25,
                        LineTotal = 50
                    }
                }
                };

                await _emailService.SendOrderConfirmationEmail("sandeshchrestha@gmail.com", testOrder);
                return Ok("Test email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send test email: {ex.Message}");
            }
        }
    }
}
