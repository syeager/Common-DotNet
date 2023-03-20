using Unleash;
using Unleash.Internal;
using Unleash.Variants;

namespace LittleByte.Common.AspNet.Unleash;

public sealed class AlwaysEnabledUnleash : IUnleash
{
    public void Dispose() { }
    public bool IsEnabled(string toggleName) => true;
    public bool IsEnabled(string toggleName, bool defaultSetting) => true;
    public bool IsEnabled(string toggleName, UnleashContext context) => true;
    public bool IsEnabled(string toggleName, UnleashContext context, bool defaultSetting) => true;
    public Variant GetVariant(string toggleName) => throw new NotImplementedException();
    public Variant GetVariant(string toggleName, Variant defaultValue) => throw new NotImplementedException();
    public Variant GetVariant(string toggleName, UnleashContext context, Variant defaultValue) => throw new NotImplementedException();
    public IEnumerable<VariantDefinition> GetVariants(string toggleName) => throw new NotImplementedException();
    public IEnumerable<VariantDefinition> GetVariants(string toggleName, UnleashContext context) => throw new NotImplementedException();
    public void ConfigureEvents(Action<EventCallbackConfig> config) => throw new NotImplementedException();
    public ICollection<FeatureToggle> FeatureToggles { get; } = Array.Empty<FeatureToggle>();
}
