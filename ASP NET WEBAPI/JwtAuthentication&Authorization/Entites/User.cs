using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationAuthorization.Entites
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Roles { get; set; } = "User";

        // Refresh token ka HASH (asli token DB me kabhi nahi rakhte)
        public string? RefreshTokenHash { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}