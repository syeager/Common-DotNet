using Serilog.Core;
using Serilog.Events;

namespace LittleByte.Serilog;

public sealed class RemoveSourceContextEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.RemovePropertyIfPresent("SourceContext");
    }
}