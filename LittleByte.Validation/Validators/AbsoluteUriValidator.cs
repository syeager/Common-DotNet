using System;
using FluentValidation;
using JetBrains.Annotations;

namespace LittleByte.Validation
{
    [UsedImplicitly]
    public class AbsoluteUriValidator : AbstractValidator<Uri>
    {
        public AbsoluteUriValidator()
        {
            RuleFor(u => u).Must(u => u.IsAbsoluteUri);
        }
    }
}