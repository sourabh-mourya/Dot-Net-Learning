
namespace JwtAuthenticationAuthorization.Models
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Roles { get; set; } = "User";
    }
}