using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : BaseController
{
    public OrdersController(ILogger<BaseController> logger) : base(logger) { }

    [HttpPost]
    [Authorize]
    public IActionResult PlaceOrder() => Ok();

    [HttpPut("cancel/{orderId}")]
    [Authorize]
    public IActionResult CancelOrder(int orderId) => Ok();

    [HttpGet("{orderId}")]
    [Authorize]
    public IActionResult GetOrderDetails(int orderId) => Ok();

    [HttpGet("history")]
    [Authorize]
    public IActionResult OrderHistory() => Ok();
}