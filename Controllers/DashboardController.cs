using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : BaseController
{
    public DashboardController(ILogger<BaseController> logger) : base(logger) { }

    [HttpGet("notifications")]
    [Authorize]
    public IActionResult GetNotifications() => Ok();
}