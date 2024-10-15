using Microsoft.AspNetCore.Mvc;

public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // Giả sử xác thực username và password
        if (model.Username == "admin" && model.Password == "password")
        {
            var token = _jwtService.GenerateToken(model.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
