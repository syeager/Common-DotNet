using System;

namespace LittleByte.Core.Dates
{
    public readonly struct DateRange
    {
        public DateTime Begin { get; }
        public DateTime End { get; }

        public TimeSpan Length => End - Begin;

        public DateRange(DateTime begin, DateTime end)
        {
            Begin = begin;
            End = end;
        }

        public void Deconstruct(out DateTime begin, out DateTime end)
        {
            begin = Begin;
            end = End;
        }
    }
}