namespace JwtAuthenticationAuthorization.Entites
{
public class User
{
public string Username { get; set; } = string.Empty;
public string PasswordHash { get; set; } = string.Empty;
}
}

user ko create kr liya hi db me password ko hash krke rkhege

ab apne direct isko request me to bhej nhi skte hi isss liye apne iska ek dto ko create krenge...

namespace JwtAuthenticationAuthorization.Models
{
public class UserDto
{
public string Username { get; set; } = string.Empty;
public string Password { get; set; } = string.Empty;
}
}

yaha pr password hash krne ke koi jaurat nhi hi

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public async Task<User?> RegisterAsync(UserDto request)
        {
            if (await myDbContext.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }
            var user = new User();

            user.Username = request.Username;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            await myDbContext.Users.AddAsync(user);//ye database me save kr rha hi ...
            await myDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<string> LoginAsync(UserDto request)
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
            string token = CreateToken(user);
            return token;
        }



        private string CreateToken(User user)
        {
            // ---------- STEP 1: CLAIMS ----------
            // Claim = token ke andar rakhi hui ek chhoti information (key-value).
            // Ye "user ki pehchaan" hai, jo server har request pe token se wapas padhta hai.
            // IMPORTANT: JWT encrypted nahi hota, koi bhi decode karke padh sakta hai (jwt.io pe).
            // Isliye password ya koi secret cheez claim me kabhi mat daalo.
            var claims = new List<Claim>{
                // Username
        new Claim(ClaimTypes.Name, user.Username)  };

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
                expires: DateTime.UtcNow.AddMinutes(60),

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
to apne Service bana saar code yaha token creation login and everything

apne db ke saath bhi myDbContextClass bana
code first approach ka used kiya hi

then apne isko direct controller ke ander used kr lege

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthenticationAuthorization.Entites;
using JwtAuthenticationAuthorization.Models;
using JwtAuthenticationAuthorization.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationAuthorization.Controllers
{
[Route("api/[Controller]")]
[ApiController]
public class AuthController : ControllerBase
{
private readonly IAuthService service;
public AuthController(IAuthService service)
{
this.service = service;
}

        [HttpPost("/register")]
        public async Task<ActionResult<User?>> Register(UserDto request)
        {
            var user = await service.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("User name already exits");
            }
            return Ok(user);
        }

        [HttpPost("/login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {

            var token = await service.LoginAsync(request);
            if (token is null) { return BadRequest("Username/Password is wrong"); }
            return Ok(token);
        }




    }

}

ab apne ye dekhege ki login end point only protecte roles hi kon hit kr payega
means authorized user hi uss end point ko hit kr skega...

[Authorize] ye authorize keyword iska ye
iska mtlb hi ki ye user authorized hi ki nhi wha se mujhe token bheja jyega...

to iss chiz ka pura step up hota hi program.cs file ke ander ..

// ---------- JWT AUTHENTICATION ----------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option =>
{
option.TokenValidationParameters = new TokenValidationParameters
{
ValidateIssuer = true,
ValidateAudience = true,
ValidateLifetime = true,
ValidateIssuerSigningKey = true,
ValidIssuer = builder.Configuration["AppSettings:Issuer"],
ValidAudience = builder.Configuration["AppSettings:Audience"],
IssuerSigningKey = new SymmetricSecurityKey(
Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
ClockSkew = TimeSpan.Zero
};
});

agar apne swaagger used kr rhe to apne usme header ka button enable krna padega..

// ---------- SWAGGER + AUTHORIZE BUTTON ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
// Swagger ko batao ki is API me "Bearer" token se authentication hota hai
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
Type = SecuritySchemeType.Http,
Scheme = "bearer",
BearerFormat = "JWT",
In = ParameterLocation.Header,
Description = "Sirf token paste karo (Bearer likhne ki zaroorat nahi)"
});

    // Authorize dabane ke baad har request ke saath ye token jayega
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });

});


//ab apne dekhege ki only required roles hi uss route ko hit kr skege...

