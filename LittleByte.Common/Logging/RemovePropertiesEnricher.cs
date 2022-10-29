using Serilog.Core;
using Serilog.Events;

namespace LittleByte.Common.Logging;

public class RemovePropertiesEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.RemovePropertyIfPresent("SourceContext");
    }
}