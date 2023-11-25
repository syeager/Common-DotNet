using LittleByte.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unleash;
using Unleash.ClientFactory;

namespace LittleByte.Unleash;

public static class FeatureFlagConfiguration
{
    public static IServiceCollection AddFeatureFlags(this IServiceCollection @this, IConfiguration configuration)
    {
        var options = @this.BindAndGetOptions<UnleashOptions>(configuration);

        IUnleash unleash;
        if(options.UseAlwaysTrue)
        {
            unleash = new AlwaysEnabledUnleash();
        }
        else
        {
            var unleashFactory = new UnleashClientFactory();
            unleash = unleashFactory.CreateClient(options, true);
        }

        return @this.AddSingleton(unleash);
    }
}