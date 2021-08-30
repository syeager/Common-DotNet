using System;
using FluentValidation;

namespace LittleByte.Validation
{
    public class AbsoluteUriValidator : AbstractValidator<Uri>
    {
        public AbsoluteUriValidator()
        {
            RuleFor(u => u).Must(u => u.IsAbsoluteUri);
        }
    }
}