using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace LittleByte.Identity.Extensions;

public static class IdentityResultExtension
{
    public static string ToErrorString(this IdentityResult @this) =>
        string.Join(",", @this.Errors.Select(e => $"{e.Code}: {e.Description}"));
}
