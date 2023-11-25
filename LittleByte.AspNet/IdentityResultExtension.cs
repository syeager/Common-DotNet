using Microsoft.AspNetCore.Identity;

namespace LittleByte.AspNet;

public static class IdentityResultExtension
{
    public static string ToErrorString(this IdentityResult @this)
    {
        return string.Join(",", @this.Errors.Select(e => $"{e.Code}: {e.Description}"));
    }
}