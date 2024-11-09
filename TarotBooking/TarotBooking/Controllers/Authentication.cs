using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Service.Model.ReaderModel;
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
            return Redirect($"http://localhost:5173/");
        }

        return Unauthorized(new { success = false, message = "Authentication failed." });
    }

    [HttpGet("token")]
    public IActionResult GetToken()
    {
        var response = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;
        if (response.Principal == null )
        {
            return Unauthorized(new { success = false, message = "User is not authenticated." });
        }

        var fullName = response.Principal.FindFirstValue(ClaimTypes.Name);
        var email = response.Principal.FindFirstValue(ClaimTypes.Email);

        var token = _jwtService.RegisterOrAuthenticateUser(fullName, email);

        if (token != null)
        {
            return Ok(new { success = true, token });
        }

        return BadRequest(new { success = false, message = "Token generation failed." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest(new { success = false, message = "Email and password are required." });
        }

        try
        {
            var token = await _jwtService.LoginWithEmailAndPassword(loginRequest.Email, loginRequest.Password);

            return Ok(new { success = true, token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { success = false, message = ex.Message });
        }
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok(new { success = true, message = "Logout successful." });
    }
}
