using AD_Coursework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StaffController : BaseController
{
    public StaffController(ILogger<BaseController> logger) : base(logger) { }

    [HttpPut("claims/{claimCode}")]
    [Authorize(Roles = "Staff")]
    public IActionResult FulfillOrder(string claimCode) => Ok();
}