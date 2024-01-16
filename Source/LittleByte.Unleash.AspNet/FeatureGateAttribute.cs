using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Unleash;

namespace LittleByte.Unleash.AspNet;

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
