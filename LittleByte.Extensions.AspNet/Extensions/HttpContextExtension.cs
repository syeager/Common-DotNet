using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LittleByte.Extensions.AspNet.Extensions
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
