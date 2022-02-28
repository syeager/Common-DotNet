using System;
using Unleash;

namespace LittleByte.Extensions.AspNet.Unleash;

public class UnleashOptions
{
    public bool UseAlwaysTrue { get; init; }
    public string ApiToken { get; init; } = null!;
    public string AppName { get; init; } = null!;
    public string Environment { get; init; } = null!;
    public string InstanceTag { get; init; } = null!;
    public string ProjectId { get; init; } = null!;
    public string UnleashApi { get; init; } = null!;

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
