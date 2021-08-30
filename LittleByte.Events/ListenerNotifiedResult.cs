using System;
using System.Threading.Tasks;

namespace LittleByte.Events
{
    public readonly struct ListenerNotifiedResult
    {
        public Type ListenerType { get; }
        public TaskStatus Status { get; }
        public long DurationMs { get; }
        public Exception? Exception { get; }

        public ListenerNotifiedResult(IEventListener listener, Exception? exception, long durationMs, TaskStatus status)
        {
            ListenerType = listener.GetType();
            Exception = exception;
            DurationMs = durationMs;
            Status = status;
        }
    }
}