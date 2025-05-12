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

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
            : base(logger)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            try
            {
                if (registrationDto == null)
                {
                    return Error(
                        "Request body cannot be empty.",
                        StatusCodes.Status400BadRequest
                    );
                }

                if (!ModelState.IsValid)
                {
                    return Error
                    (
                        "Invalid data. Please check your details.",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }

                var result = await _authService.RegisterAsync(registrationDto);
                if (!result.IsSuccess)
                {
                    return Error
                    (
                        result.Message ?? "Registration failed. Please try again.",
                        StatusCodes.Status400BadRequest
                    );
                }

                return Success<Object>
                (
                    null, 
                    "Registration successful!", 
                    StatusCodes.Status201Created
                );
            }
            catch (Exception ex)
            {
                return HandleException
                (
                    ex,
                    "Something went wrong. Please try again."
                );
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    return Error(
                        "Request body cannot be empty.",
                        StatusCodes.Status400BadRequest
                    );
                }

                if (!ModelState.IsValid)
                {
                    return Error
                    (
                        "Invalid login data",
                        StatusCodes.Status400BadRequest,
                        ModelState
                    );
                }
                var result = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
                if (!result.AuthResponse.IsSuccess)
                {
                    return Error
                    (
                        result.AuthResponse.Message, 
                        StatusCodes.Status400BadRequest
                    );
                }

                var flattenedResponse = new
                {
                    token = result.AuthResponse.Token,
                    refreshToken = result.AuthResponse.RefreshToken,
                    userId = result.AuthResponse.UserId,
                    role = result.AuthResponse.Role
                };

                return Success(flattenedResponse, "Login successful!");
            }
            catch (Exception ex)
            {
                return HandleException
                (
                    ex,
                    "Something went wrong. Please try again."
                );
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequest)
        {
            try
            {
                var result = await _authService.RefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken);
                if (!result.IsSuccess)
                {
                    return Error
                    (
                        result.Message, 
                        StatusCodes.Status400BadRequest
                    );
                }

                return Success(
                    result,
                    "Token refreshed successfully."
                );
            }
            catch (Exception ex)
            {
                return HandleException
                (
                    ex,
                    "Something went wrong. Please try again."
                );
            }
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken()
        {
            try
            {
                var userId = User.GetUserId();
                await _authService.RevokeTokenAsync(userId);
                return Success<object>
                (
                    null,
                    "Token revoked successfully"
                );
            }
            catch (Exception ex)
            {
                return HandleException
                (
                    ex,
                    "Something went wrong. Please try again."
                );
            }
        }
    }
}