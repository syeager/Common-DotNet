using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LittleByte.Common;

// https://medium.com/@cilliemalan/how-to-await-a-cancellation-token-in-c-cbfc88f28fa2
public static class CancellationTokenExtension
{
    /// <summary>
    ///     Allows a cancellation token to be awaited.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static CancellationTokenAwaiter GetAwaiter(this CancellationToken ct)
    {
        return new CancellationTokenAwaiter(ct);
    }
}

/// <summary>
///     The awaiter for cancellation tokens.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public readonly record struct CancellationTokenAwaiter(CancellationToken CancellationToken) : ICriticalNotifyCompletion
{
    // called by compiler generated/.net internals to check
    // if the task has completed.
    public bool IsCompleted => CancellationToken.IsCancellationRequested;

    // The compiler will generate stuff that hooks in
    // here. We hook those methods directly into the
    // cancellation token.
    public void OnCompleted(Action continuation)
    {
        CancellationToken.Register(continuation);
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        CancellationToken.Register(continuation);
    }

    public object GetResult()
    {
        // this is called by compiler generated methods when the
        // task has completed. Instead of returning a result, we 
        // just throw an exception.
        if (IsCompleted)
        {
            throw new OperationCanceledException();
        }

        throw new InvalidOperationException("The cancellation token has not yet been cancelled.");
    }
}