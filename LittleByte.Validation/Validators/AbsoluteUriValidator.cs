using System;
using FluentValidation;
using FluentValidation.Validators;
using JetBrains.Annotations;

namespace LittleByte.Validation.Validators;

public static class AbsoluteUriValidatorExtension
{
    public static IRuleBuilderOptions<T, Uri> IsAbsoluteUri<T>(this IRuleBuilder<T, Uri> @this)
    {
        return @this.SetValidator(new AbsoluteUriValidator<T>());
    }
}

[UsedImplicitly]
public class AbsoluteUriValidator<T> : PropertyValidator<T, Uri>
{
    public override string Name => nameof(AbsoluteUriValidator<T>);

    public override bool IsValid(ValidationContext<T> context, Uri value)
    {
        return value.IsAbsoluteUri;
    }
}
