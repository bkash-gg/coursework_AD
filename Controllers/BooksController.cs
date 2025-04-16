using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : BaseController
{
    public BooksController(ILogger<BaseController> logger) : base(logger) { }

    [HttpGet]
    public IActionResult GetBooks() => Ok();

    [HttpGet("{id}")]
    public IActionResult GetBookById(int id) => Ok();

    [HttpGet("categories/{category}")]
    public IActionResult GetBooksByCategory(string category) => Ok();

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult AddBook() => Ok();

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateBook(int id) => Ok();

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteBook(int id) => Ok();
}