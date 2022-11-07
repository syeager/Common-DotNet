using Serilog.Core;
using Serilog.Events;

namespace LittleByte.Common.Logging;

public class RemoveSourceContextEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.RemovePropertyIfPresent("SourceContext");
    }
}