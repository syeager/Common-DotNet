using System;
using Serilog.Context;

namespace LittleByte.Logging
{
    public static class Keys
    {
        public static string New(params string[] keys) => string.Join('.', keys);
    }

    public interface ILog : IDisposable
    {
        public ILog Push(string name, object? value);
    }

    internal sealed class Log : ILog
    {
        private IDisposable? rootLogContext;

        public ILog Push(string name, object? value)
        {
            var context = LogContext.PushProperty(name, value);
            rootLogContext ??= context;
            return this;
        }

        public void Dispose()
        {
            rootLogContext?.Dispose();
        }
    }

    public static class Logs
    {
        public static ILog Props() => new Log();
    }
}