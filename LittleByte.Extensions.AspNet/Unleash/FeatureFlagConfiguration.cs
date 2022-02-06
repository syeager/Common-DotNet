using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unleash.ClientFactory;

namespace LittleByte.Extensions.AspNet.Unleash;

public static class FeatureFlagConfiguration
{
    public static IServiceCollection AddFeatureFlags(this IServiceCollection @this, IConfiguration configuration)
    {
        var options = configuration.GetValue<UnleashOptions>(nameof(UnleashOptions));
        var unleashFactory = new UnleashClientFactory();
        var unleash = unleashFactory.CreateClient(options, true);
        return @this.AddSingleton(unleash);
    }
}
