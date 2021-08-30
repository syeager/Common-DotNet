using System;
using FluentValidation;
using LittleByte.Core.Dates;

namespace LittleByte.Validation
{
    public class DateRangeValidator : AbstractValidator<DateRange>
    {
        public DateRangeValidator()
        {
            RuleFor(dr => dr.Length).GreaterThanOrEqualTo(TimeSpan.Zero);
        }
    }
}