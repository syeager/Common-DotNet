using FluentValidation;
using LittleByte.Common;

namespace LittleByte.Validation;

public class DateRangeValidator : AbstractValidator<DateRange>
{
    public DateRangeValidator()
    {
        RuleFor(dr => dr.Length).GreaterThanOrEqualTo(TimeSpan.Zero);
    }
}