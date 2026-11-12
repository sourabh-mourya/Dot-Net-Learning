using JwtAuthenticationAuthorization.Entites;
using JwtAuthenticationAuthorization.Models;

namespace JwtAuthenticationAuthorization.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDto request);
        Task<TokenResponseDto?> LoginAsync(LoginDto request);                    // ab dono token milte hain
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task<bool> LogoutAsync(Guid userId);
    }
}