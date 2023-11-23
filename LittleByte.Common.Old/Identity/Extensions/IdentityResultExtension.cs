using Microsoft.AspNetCore.Identity;

namespace LittleByte.Common.Identity.Extensions;

public static class IdentityResultExtension
{
    public static string ToErrorString(this IdentityResult @this) =>
        string.Join(",", @this.Errors.Select(e => $"{e.Code}: {e.Description}"));
}
