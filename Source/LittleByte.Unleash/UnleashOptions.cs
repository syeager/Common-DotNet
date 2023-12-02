using Unleash;

namespace LittleByte.Unleash;

public record UnleashOptions(
    bool UseAlwaysTrue,
    string ApiToken,
    string AppName,
    string Environment,
    string InstanceTag,
    string ProjectId,
    string UnleashApi
)
{
    public static implicit operator UnleashSettings(UnleashOptions @this)
    {
        return new UnleashSettings
        {
            AppName = @this.AppName,
            InstanceTag = @this.InstanceTag,
            UnleashApi = new Uri(@this.UnleashApi),
            CustomHttpHeaders = new Dictionary<string, string>
            {
                {"Authorization", @this.ApiToken},
            },
            ProjectId = @this.ProjectId,
            Environment = @this.Environment,
        };
    }
}