using FluentValidation;
using LittleByte.Common.Dates;

namespace LittleByte.Common.Validation.Validators
{
    public class DateRangeValidator : AbstractValidator<DateRange>
    {
        public DateRangeValidator()
        {
            RuleFor(dr => dr.Length).GreaterThanOrEqualTo(TimeSpan.Zero);
        }
    }
}
