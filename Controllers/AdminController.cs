using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminController : BaseController
{
    public AdminController(ILogger<BaseController> logger) : base(logger) { }

    [HttpPost("discounts")]
    [Authorize(Roles = "Admin")]
    public IActionResult SetDiscount() => Ok();

    [HttpPost("announcements")]
    [Authorize(Roles = "Admin")]
    public IActionResult SetAnnouncement() => Ok();
}