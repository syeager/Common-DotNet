using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace LittleByte.Asp.Identity
{
    public static class IdentityResultExtension
    {
        public static string ToErrorString(this IdentityResult identityResult)
        {
            return string.Join(",", identityResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
        }
    }
}