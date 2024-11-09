using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace BE.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var userRoleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (string.IsNullOrEmpty(userRoleClaim))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!_roles.Contains(userRoleClaim))
            {
                context.Result = new ForbidResult();
                return;
            }

            await Task.CompletedTask;
        }
    }

    public static class Roles
    {
        public const string Admin = "2";
        public const string User = "1";
        public const string Reader = "3";
    }
}