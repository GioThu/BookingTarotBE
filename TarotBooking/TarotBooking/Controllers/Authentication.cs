using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TarotBooking.Model;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtService _jwtService;

    public AuthController(IAuthService authService, JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpGet("redirect")]
    public IActionResult GoogleRedirect()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleLogin")
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("signin-google")]
    public async Task<IActionResult> GoogleLogin()
    {
        var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (response.Principal == null)
        {
            return BadRequest(new { success = false, message = "No principal found." });
        }

        var fullName = response.Principal.FindFirstValue(ClaimTypes.Name);
        var email = response.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new { success = false, message = "Email is required." });
        }

        var token = _jwtService.RegisterOrAuthenticateUser(fullName, email);

        if (token != null)
        {
            return Redirect("http://localhost:5173/");
        }

        return Unauthorized(new { success = false, message = "Authentication failed." });
    }

    [HttpGet("token")]
    public IActionResult GetToken()
    {
        // Kiểm tra xem người dùng đã được xác thực chưa
        var response = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;
        if (response.Principal == null )
        {
            return Unauthorized(new { success = false, message = "User is not authenticated." });
        }

        // Lấy thông tin từ claims
        var fullName = response.Principal.FindFirstValue(ClaimTypes.Name);
        var email = response.Principal.FindFirstValue(ClaimTypes.Email);

        // Tạo token cho người dùng
        var token = _jwtService.RegisterOrAuthenticateUser(fullName, email);

        if (token != null)
        {
            return Ok(new { success = true, token });
        }

        return BadRequest(new { success = false, message = "Token generation failed." });
    }
}
