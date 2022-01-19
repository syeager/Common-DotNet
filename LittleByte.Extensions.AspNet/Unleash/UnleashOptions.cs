using System;
using Unleash;

namespace LittleByte.Extensions.AspNet.Unleash;

public record UnleashOptions(
    string ApiToken,
    string AppName,
    string Environment,
    string InstanceTag,
    string ProjectId,
    string UnleashApi)
{
    public static implicit operator UnleashSettings(UnleashOptions @this)
    {
        return new UnleashSettings
        {
            AppName = @this.AppName,
            InstanceTag = @this.InstanceTag,
            UnleashApi = new Uri(@this.UnleashApi),
            CustomHttpHeaders = new()
            {
                {"Authorization", @this.ApiToken}
            },
            ProjectId = @this.ProjectId,
            Environment = @this.Environment,
        };
    }
}
