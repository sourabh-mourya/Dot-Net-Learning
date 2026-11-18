using System.Security.Claims;
using JwtAuthenticationAuthorization.Models;
using JwtAuthenticationAuthorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var user = await authService.RegisterAsync(request);

            if (user is null)
                return Conflict("Username already exists");

            return Ok(new { user.Id, user.Username, user.Roles });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var tokens = await authService.LoginAsync(request);

            if (tokens is null)
                return Unauthorized("Invalid username or password");

            return Ok(tokens);   // { accessToken, refreshToken }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            var tokens = await authService.RefreshTokensAsync(request);

            if (tokens is null)
                return Unauthorized("Invalid or expired refresh token");

            return Ok(tokens);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // User ki id token ke NameIdentifier claim se aati hai
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            await authService.LogoutAsync(userId);
            return Ok("Logged out");
        }

        [HttpGet("user-endpoint")]
        [Authorize]
        public IActionResult UserEndpoint()
        {
            return Ok("Logged-in user: " + User.Identity?.Name);
        }

        [HttpGet("admin-endpoint")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            return Ok("Admin: " + User.Identity?.Name);
        }
    }
}

































