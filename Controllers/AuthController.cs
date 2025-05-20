using Microsoft.AspNetCore.Authorization;
using AD_Coursework.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using AD_Coursework.Extensions;
using AD_Coursework.DTOs.Auth;
using AD_Coursework.DTOs.User;

namespace AD_Coursework.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        // Constructor that initializes the authentication service and logger
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
            : base(logger)
        {
            _authService = authService;
        }

        // Handles user registration by processing and validating registration information
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            try
            {
                if (registrationDto == null)
                {
                    return Error("Please provide your registration details.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error("Invalid input. Please check your details and try again.", StatusCodes.Status400BadRequest, ModelState);
                }

                var result = await _authService.RegisterAsync(registrationDto);
                if (!result.IsSuccess)
                {
                    return Error(result.Message ?? "Registration failed. Please try again later.", StatusCodes.Status400BadRequest);
                }

                return Success<object>(null, "Account created successfully. You can now log in.", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user registration.");
                return HandleException(ex, "We couldn’t process your registration. Please try again later.");
            }
        }

        // Authenticates users by validating credentials and generating auth tokens
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    return Error("Login details are missing.", StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    return Error("Invalid login information.", StatusCodes.Status400BadRequest, ModelState);
                }

                var result = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
                if (!result.AuthResponse.IsSuccess)
                {
                    return Error(result.AuthResponse.Message ?? "Unable to log in. Please check your credentials.", StatusCodes.Status400BadRequest);
                }

                var flattenedResponse = new
                {
                    token = result.AuthResponse.Token,
                    refreshToken = result.AuthResponse.RefreshToken,
                    userId = result.AuthResponse.UserId,
                    role = result.AuthResponse.Role,
                    email = result.AuthResponse.Email
                };

                return Success(flattenedResponse, "You’ve successfully logged in.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed due to an unexpected error.");
                return HandleException(ex, "We couldn’t log you in. Please try again shortly.");
            }
        }

        // Issues new access and refresh tokens based on a valid refresh token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequest)
        {
            try
            {
                var result = await _authService.RefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken);
                if (!result.IsSuccess)
                {
                    return Error(result.Message ?? "Unable to refresh token. Please log in again.", StatusCodes.Status400BadRequest);
                }

                return Success(result, "Your session has been renewed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while refreshing token.");
                return HandleException(ex, "We couldn’t refresh your session. Please try again shortly.");
            }
        }

        // Invalidates the current user's refresh token to force re-authentication
        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken()
        {
            try
            {
                var userId = User.GetUserId();
                await _authService.RevokeTokenAsync(userId);
                return Success<object>(null, "You’ve been logged out from this session.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while revoking token.");
                return HandleException(ex, "We couldn’t complete the logout. Please try again.");
            }
        }
    }
}