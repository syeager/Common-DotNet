using System;
using Serilog.Context;

namespace LittleByte.Logging
{
    public interface ILog : IDisposable
    {
        public ILog Push(string name, object? value);
        public ILog DiagnosticPush(string name, object? value);
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

        public ILog DiagnosticPush(string name, object? value)
        {
            Logs.DiagnosticContext.Set(name, value);
            return Push(name, value);
        }

        public void Dispose()
        {
            rootLogContext?.Dispose();
        }
    }
}