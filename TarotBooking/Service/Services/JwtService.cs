using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using TarotBooking.Services.Interfaces;
using TarotBooking.Models;
using System.Globalization;

public class JwtService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IReaderService _readerService;
    private readonly IImageService _imageService;

    public JwtService(IConfiguration configuration, IUserService userService, IReaderService readerService, IImageService imageService)
    {
        _configuration = configuration;
        _userService = userService;
        _readerService = readerService;
        _imageService = imageService;
    }

    public async Task<string> RegisterOrAuthenticateUser(string fullname, string email)
    {
        fullname = RemoveDiacritics(fullname);
        var existingUser = await _userService.GetUserWithEmail(email);

        if (existingUser == null)
        {
            var newUser = await _userService.CreateUser(fullname, email);
            return GenerateToken(fullname, email, newUser.Role, newUser.Id, null);
        }

        Image? latestImage = await _imageService.FindLatestImageUserAsync(existingUser.Id);
        var latestImageUrl = latestImage?.Url;

        return GenerateToken(fullname, email, existingUser.Role, existingUser.Id, latestImageUrl);
    }

    public async Task<string> LoginWithEmailAndPassword(string email, string password)
    {
        var reader = await _readerService.ValidateReaderCredentials(email, password);
        if (reader.Status != "Active") throw new Exception("Blocked User");


        if (reader == null || reader.Password != password)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        Image? latestImage = await _imageService.FindLatestImageReaderAsync(reader.Id);
        var latestImageUrl = latestImage?.Url;
        return GenerateToken(reader.Name, reader.Email, 3, reader.Id, latestImageUrl);
    }

    private string GenerateToken(string fullname, string email, int? role, string userId, string latestImageUrl)
    {
        if (latestImageUrl == null)
        {
            latestImageUrl = "not found";
        }
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, fullname),
            new Claim(JwtRegisteredClaimNames.Email, email),

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id", userId),
            new Claim("Role", role.ToString()),
            new Claim("Image", latestImageUrl)

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

    public static string RemoveDiacritics(string text)
    {
        string normalizedString = text.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

}
