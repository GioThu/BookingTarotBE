using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public interface IAuthService
{
    Task<string> RegisterWithGoogleAsync(HttpContext httpContext);
    string GenerateJwtToken(Claim[] claims);
}