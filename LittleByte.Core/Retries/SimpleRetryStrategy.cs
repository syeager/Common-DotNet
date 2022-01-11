using System;
using System.Threading.Tasks;

namespace LittleByte.Core.Retries;

public sealed class SimpleRetryStrategy : RetryStrategy
{
    protected override async ValueTask RunAsyncInternal(Action action, RetrySettings settings)
    {
        var remainingAttempts = settings.Attempts;

        while(remainingAttempts >= 0)
        {
            --remainingAttempts;

            bool didSucceed;
            try
            {
                action();
                didSucceed = true;
            }
            catch(Exception)
            {
                // log
                didSucceed = false;
            }

            if(didSucceed) return;

            await Task.Delay(settings.DelayMs);
        }

        throw new Exception();
    }

    protected override async ValueTask<T?> RunAsyncInternal<T>(Func<T> action, RetrySettings settings) where T : class
    {
        var remainingAttempts = settings.Attempts;
        var result = default(T);

        while(remainingAttempts >= 0)
        {
            --remainingAttempts;

            bool didSucceed;
            try
            {
                result = action();
                didSucceed = true;
            }
            catch(Exception)
            {
                // log
                didSucceed = false;
            }

            if(didSucceed) return result;

            await Task.Delay(settings.DelayMs);
        }

        throw new Exception();
    }
}
