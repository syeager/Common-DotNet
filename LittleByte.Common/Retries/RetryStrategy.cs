namespace LittleByte.Common.Retries;

public record RetrySettings(int Attempts = 3, int DelayMs = 3000);

public abstract class RetryStrategy
{
    protected abstract ValueTask RunAsyncInternal(Action action, RetrySettings settings);
    protected abstract ValueTask<T?> RunAsyncInternal<T>(Func<T> action, RetrySettings settings) where T : class;

    public ValueTask RunAsync(Action action, RetrySettings? settings = null)
    {
        settings ??= new RetrySettings();
        return RunAsyncInternal(action, settings);
    }

    public ValueTask<T?> RunAsync<T>(Func<T> action, RetrySettings? settings = null) where T : class
    {
        settings ??= new RetrySettings();
        return RunAsyncInternal(action, settings);
    }
}
