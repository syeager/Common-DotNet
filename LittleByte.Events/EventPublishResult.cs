using System;

namespace LittleByte.Events
{
    public readonly struct EventPublishResult
    {
        public object Initiator { get; }
        public DateTime StartTime { get; }
        public int SuccessfulListeners { get; }
        public int TotalListeners { get; }

        public EventPublishResult(object initiator, DateTime startTime, int successfulListeners, int totalListeners)
        {
            Initiator = initiator;
            StartTime = startTime;
            SuccessfulListeners = successfulListeners;
            TotalListeners = totalListeners;
        }
    }
}