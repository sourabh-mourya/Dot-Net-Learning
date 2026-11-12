using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;   // NEW: refresh token (random bytes aur SHA256) ke liye
using System.Text;
using JwtAuthenticationAuthorization.Data;
using JwtAuthenticationAuthorization.Entites;
using JwtAuthenticationAuthorization.Models;
using JwtAuthenticationAuthorization.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationAuthorization.Controllers
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly MyDbContext myDbContext;
        public AuthService(IConfiguration configuration, MyDbContext myDbContext)
        {
            this.configuration = configuration;
            this.myDbContext = myDbContext;
        }

        public async Task<User?> RegisterAsync(RegisterDto request)
        {
            if (await myDbContext.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }

            // Sirf ye roles allowed, koi aur string aaye to "User" ban jayega
            var allowedRoles = new[] { "User", "Admin", "Manager" };
            string role = allowedRoles.Contains(request.Roles) ? request.Roles : "User";

            var user = new User();
            user.Username = request.Username;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.Roles = role;

            await myDbContext.Users.AddAsync(user);   // DB me add
            await myDbContext.SaveChangesAsync();     // DB me save
            return user;
        }

        // CHANGED: return type string? se TokenResponseDto? ho gaya (access + refresh dono milte hain)
        public async Task<TokenResponseDto?> LoginAsync(LoginDto request)
        {
            User? user = await myDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user is null)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            // CHANGED: pehle sirf CreateToken(user) hota tha, ab access + refresh dono banenge
            return await CreateTokenResponseAsync(user);
        }

        // ================= NEW: REFRESH =================
        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            // Client ne jo refresh token bheja uska hash banao, aur DB me usi hash wala user dhundo
            string hash = HashToken(request.RefreshToken);

            var user = await myDbContext.Users.FirstOrDefaultAsync(u => u.RefreshTokenHash == hash);

            // User nahi mila (galat/purana token) ya refresh token expire ho gaya
            if (user is null || user.RefreshTokenExpiryTime is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            // Naya access token + NAYA refresh token (rotation). Purana hash overwrite ho jata hai,
            // isliye purana refresh token dobara use nahi ho sakta.
            return await CreateTokenResponseAsync(user);
        }

        // ================= NEW: LOGOUT =================
        public async Task<bool> LogoutAsync(Guid userId)
        {
            var user = await myDbContext.Users.FindAsync(userId);
            if (user is null) return false;

            // Refresh token hata do. Ab refresh nahi ho sakta, access token expire hote hi session khatam
            user.RefreshTokenHash = null;
            user.RefreshTokenExpiryTime = null;
            await myDbContext.SaveChangesAsync();
            return true;
        }

        // ================= NEW: ACCESS + REFRESH TOKEN RESPONSE =================
        private async Task<TokenResponseDto> CreateTokenResponseAsync(User user)
        {
            // Step 1: naya random refresh token banao
            string refreshToken = GenerateRefreshToken();

            // Step 2: uska HASH DB me save karo, asli token nahi
            int expiryDays = configuration.GetValue<int>("AppSettings:RefreshTokenExpiryDays", 7);
            user.RefreshTokenHash = HashToken(refreshToken);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(expiryDays);
            await myDbContext.SaveChangesAsync();

            // Step 3: access token + asli refresh token client ko do
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = refreshToken
            };
        }

        // ================= NEW: RANDOM REFRESH TOKEN =================
        private static string GenerateRefreshToken()
        {
            // 64 random bytes. JWT nahi hai, bas ek unpredictable string hai
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        // ================= NEW: REFRESH TOKEN KA HASH =================
        private static string HashToken(string token)
        {
            // SHA256 kaafi hai, kyunki refresh token already lamba aur random hota hai
            // (password ki tarah BCrypt ki zaroorat nahi)
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        }

        private string CreateToken(User user)
        {
            // ---------- STEP 1: CLAIMS ----------
            // Claim = token ke andar rakhi hui ek chhoti information (key-value).
            // Ye "user ki pehchaan" hai, jo server har request pe token se wapas padhta hai.
            // IMPORTANT: JWT encrypted nahi hota, koi bhi decode karke padh sakta hai (jwt.io pe).
            // Isliye password ya koi secret cheez claim me kabhi mat daalo.
            var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim(ClaimTypes.Name, user.Username),
    new Claim(ClaimTypes.Role, user.Roles)
};
            // ---------- STEP 2: SECRET KEY ----------
            // appsettings.json se secret string nikaalo. Ye key sirf server ke paas hoti hai.
            // Token isi key se "sign" hota hai, jisse server baad me pehchanta hai ki
            // token usne hi banaya tha aur kisi ne beech me badla nahi.
            var tokenKey = configuration["AppSettings:Token"];
            if (string.IsNullOrEmpty(tokenKey))
            {
                // Key missing hai to turant clear error, taaki baad me confusing error na aaye
                throw new InvalidOperationException("AppSettings:Token is missing in configuration");
            }

            // String ko bytes me badalo, phir SymmetricSecurityKey me daalo
            // (Symmetric = signing aur verify dono me same key use hoti hai)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            // ---------- STEP 3: SIGNING CREDENTIALS ----------
            // Kis key se aur kis algorithm se sign karna hai.
            // HmacSha256 ke liye key kam se kam 32 characters (256 bit) ki honi chahiye.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // ---------- STEP 4: TOKEN BANAO ----------
            var token = new JwtSecurityToken(
                // issuer = token KISNE banaya (tumhari API ka naam)
                issuer: configuration["AppSettings:Issuer"],

                // audience = token KISKE liye hai (tumhara client / React app)
                // Validation me issuer aur audience match na hue to token reject ho jata hai
                audience: configuration["AppSettings:Audience"],

                // Upar wale claims token ke payload me jayenge
                claims: claims,

                // Token kab expire hoga. Ek din bahut lamba hai, chori hone pe 24 ghante tak chalega.
                // Isliye chhota rakho (15-60 minute), aur lamba session chahiye to refresh token use karo.
                // CHANGED: 60 se 15 minute (ab lamba session refresh token sambhalega)
                expires: DateTime.UtcNow.AddMinutes(15),

                // Upar bana hua signature
                signingCredentials: creds
            );

            // ---------- STEP 5: STRING ME CONVERT ----------
            // Token object ko final string me badlo: header.payload.signature
            // Ye string client ko jaati hai.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}