using System;

namespace LittleByte.WorkerQueue
{
    public readonly struct WorkResult
    {
        public enum Statuses
        {
            None,
            Cancelled,
            Failed,
            TimedOut,
            Successful,
        }

        public Statuses Status { get; }
        public string Message { get; }
        public long DurationMs { get; }

        private WorkResult(Statuses status, string message, long durationMs)
        {
            Status = status;
            Message = message;
            DurationMs = durationMs;
        }

        public static WorkResult TimedOut(long durationMs) => new(Statuses.TimedOut, string.Empty, durationMs);
        public static WorkResult Cancelled(long durationMs, string message) => new(Statuses.Cancelled, message, durationMs);
        public static WorkResult Failed(long durationMs, Exception exception) => new(Statuses.Failed, exception.Message, durationMs);
        public static WorkResult Successful(long durationMs, string message = "") => new(Statuses.Successful, message, durationMs);

        public override string ToString() => $"{Status} in {DurationMs}ms with message '{Message}'.";
    }
}