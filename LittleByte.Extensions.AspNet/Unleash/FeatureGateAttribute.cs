using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Unleash;

namespace LittleByte.Extensions.AspNet.Unleash;

public class FeatureGateAttribute : ActionFilterAttribute
{
    private readonly IReadOnlyCollection<string> requiredFlags;

    public FeatureGateAttribute(params string[] requiredFlags)
    {
        this.requiredFlags = requiredFlags;
    }

    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var unleash = context.HttpContext.RequestServices.GetRequiredService<IUnleash>();
        var isEnabled = requiredFlags.All(unleash.IsEnabled);
        if(isEnabled)
        {
            return next();
        }

        context.Result = new ForbidResult();
        return Task.CompletedTask;
    }
}
