using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using TarotBooking.Services.Interfaces;
using TarotBooking.Models;

public class JwtService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService; 
    public JwtService(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    public async Task<string> RegisterOrAuthenticateUser(string fullname, string email)
    {
        var existingUser = await _userService.GetUserWithEmail(email);

        if (existingUser == null)
        {

           var newUser = await _userService.CreateUser(fullname, email);
            return GenerateToken(fullname, email, newUser.Role, newUser.Id);
        }

        return GenerateToken(fullname, email, existingUser.Role, existingUser.Id);
    }

    private string GenerateToken(string fullname, string email, int? role, string userId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, fullname),
            new Claim(JwtRegisteredClaimNames.Email, email),

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id", userId),
            new Claim("role", role.ToString())

        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWTSettings:Issuer"],
            audience: _configuration["JWTSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWTSettings:ExpireMinutes"])), 
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
