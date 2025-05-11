using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using AD_Coursework.Extensions;
using AD_Coursework.DTOs.User;

namespace AD_Coursework.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        private readonly IAuthService _authService;

        public UserProfileController(IAuthService authService, ILogger<UserProfileController> logger)
            : base(logger)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordChangeDto passwordChangeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ErrorResponse("Invalid data. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }
                _logger.LogInformation($"User claims: {string.Join(", ", User.Claims.Select(c => $"{c.Type}:{c.Value}"))}");
                var userId = User.GetUserId();
                var result = await _authService.ChangePasswordAsync(userId, passwordChangeDto);

                if (!result.IsSuccess)
                {
                    return ErrorResponse(result.Message ?? "Password change failed.", StatusCodes.Status400BadRequest);
                }

                return SuccessResponse(result, "Password changed successfully!", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Something went wrong. Please try again.");
            }
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateDto profileUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ErrorResponse("Invalid data. Please check your details.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();
                var result = await _authService.UpdateProfileAsync(userId, profileUpdateDto);

                if (!result.IsSuccess)
                {
                    return ErrorResponse(result.Message ?? "Profile update failed.", StatusCodes.Status400BadRequest);
                }

                return SuccessResponse(result, "Profile updated successfully!", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Something went wrong. Please try again.");
            }
        }
    }
}
