using Microsoft.Extensions.Logging;

namespace LittleByte.Common.Logging;

public class NullLogger<T> : ILogger<T>
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return new NullDisposable();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel,
                            EventId eventId,
                            TState state,
                            Exception? exception,
                            Func<TState, Exception?, string> formatter) { }
}