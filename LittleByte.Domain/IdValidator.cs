using FluentValidation;
using FluentValidation.Validators;

namespace LittleByte.Domain;

public static class IdValidatorExtension
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IRuleBuilderOptions<T, Id<TP>> IsNotEmpty<T, TP>(this IRuleBuilder<T, Id<TP>> @this)
    {
        return @this.SetValidator(new IdValidator<T, TP>());
    }
}

public class IdValidator<T, TP> : PropertyValidator<T, Id<TP>>
{
    public override string Name => nameof(IdValidator<T, TP>);

    public override bool IsValid(ValidationContext<T> context, Id<TP> value)
    {
        return value != Id<TP>.Empty;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "Id cannot be an empty Guid";
    }
}
