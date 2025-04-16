using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CartController : BaseController
{
    public CartController(ILogger<BaseController> logger) : base(logger) { }

    [HttpGet]
    [Authorize]
    public IActionResult GetCart() => Ok();

    [HttpPost]
    [Authorize]
    public IActionResult AddToCart() => Ok();

    [HttpPut("{bookId}")]
    [Authorize]
    public IActionResult UpdateCart(int bookId) => Ok();

    [HttpDelete("{bookId}")]
    [Authorize]
    public IActionResult RemoveFromCart(int bookId) => Ok();
}