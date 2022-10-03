using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace LittleByte.Common.AspNet.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid? GetUserId(this HttpContext httpContext)
        {
            var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
            return claim == null ? null : Guid.Parse(claim.Value);
        }
    }
}
