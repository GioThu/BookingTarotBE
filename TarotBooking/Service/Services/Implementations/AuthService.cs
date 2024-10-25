using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TarotBooking.Models;

public class AuthService : IAuthService
{
    private readonly BookingTarotContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;



    public AuthService(BookingTarotContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<string> RegisterWithGoogleAsync(HttpContext httpContext)
    {
        var httpContext2 = _httpContextAccessor.HttpContext;
        var result = await httpContext2.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (result == null)
        {
            throw new Exception("loi");
        }

        var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            user = new User
            {
                Email = email,
                Name = name,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Tạo JWT Token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return GenerateJwtToken(claims);
    }

    public async Task<string> LoginAsync(string userName, string password)
    {
        // Retrieve the user with matching email and password
        var user = await _context.Readers.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);
        if (user == null)
        {
            throw new Exception("Invalid email or password.");
        }

        // Create JWT token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return GenerateJwtToken(claims);
    }

    public string GenerateJwtToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpireTime"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}