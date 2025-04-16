using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : BaseController
{
    public ReviewsController(ILogger<BaseController> logger) : base(logger) { }

    [HttpPost("{bookId}")]
    [Authorize]
    public IActionResult AddReview(int bookId) => Ok();

    [HttpGet("{bookId}")]
    public IActionResult GetReviews(int bookId) => Ok();
}