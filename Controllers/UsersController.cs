using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    public UsersController(ILogger<BaseController> logger) : base(logger) { }

    [HttpPost("register")]
    public IActionResult Register() => Ok();

    [HttpPost("login")]
    public IActionResult Login() => Ok();

    [HttpGet("profile")]
    [Authorize]
    public IActionResult GetProfile() => Ok();

    [HttpPost("whitelist/{bookId}")]
    [Authorize]
    public IActionResult AddToWhitelist(int bookId) => Ok();

    [HttpDelete("whitelist/{bookId}")]
    [Authorize]
    public IActionResult RemoveFromWhitelist(int bookId) => Ok();

    [HttpGet("whitelist")]
    [Authorize]
    public IActionResult GetWhitelist() => Ok();
}