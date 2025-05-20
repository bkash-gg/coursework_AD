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

        // Gets the profile information of the logged-in user
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = User.GetUserId();

                var result = await _authService.GetProfileAsync(userId);

                if (!result.IsSuccess || result.Data == null)
                {
                    return Error(result.Message ?? "Couldn't retrieve your profile information.", StatusCodes.Status400BadRequest);
                }

                return Success(result.Data, "Profile retrieved successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving profile.");
                return HandleException(ex, "We couldn't retrieve your profile information. Please try again.");
            }
        }

        // Changes the password for the logged-in user
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordChangeDto passwordChangeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Please correct the highlighted errors.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();

                var result = await _authService.ChangePasswordAsync(userId, passwordChangeDto);

                if (!result.IsSuccess)
                {
                    return Error(result.Message ?? "Couldn’t update your password. Try again.", StatusCodes.Status400BadRequest);
                }

                return Success<object>(null, "Your password has been updated successfully!.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing password.");
                return HandleException(ex, "We ran into a problem changing your password. Try again.");
            }
        }

        // Updates the profile information of the logged-in user
        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateDto profileUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Error("Please correct the highlighted errors.", StatusCodes.Status400BadRequest, ModelState);
                }

                var userId = User.GetUserId();

                var result = await _authService.UpdateProfileAsync(userId, profileUpdateDto);

                if (!result.IsSuccess)
                {
                    return Error(result.Message ?? "Couldn’t update your profile. Try again.", StatusCodes.Status400BadRequest);
                }

                var flattenedResponse = new
                {
                    userId = result.UserId,
                    role = result.Role
                };

                return Success<Object>(null, "Your profile has been updated successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating profile.");
                return HandleException(ex, "We couldn’t save your profile changes. Please try again.");
            }
        }
    }
}
