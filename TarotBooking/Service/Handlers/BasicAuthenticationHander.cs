using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using TarotBooking.Models;

namespace TarotBooking.Handlers
{
    public class BasicAuthenticationHander : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly BookingTarotContext _context;

        public BasicAuthenticationHander(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            BookingTarotContext context)
            : base(options, logger, encoder, clock) 
        {
            _context = context;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header was not found");

            try
            {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                string[] credentials = System.Text.Encoding.UTF8.GetString(bytes).Split(":");
                string email = credentials[0];
                string password = credentials[1];

                User user = _context.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefault();

                if (user == null)
                    return AuthenticateResult.Fail("Invalid Email or Password");
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }    

            }

            catch (Exception) 
            {
                return AuthenticateResult.Fail("Error has occured");
            }
        }
    }
}
