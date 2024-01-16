using FluentValidation.Results;

namespace LittleByte.Validation.Test;

public class FailModelValidator<TModel> : ModelValidator<TModel>
{
    public override Valid<TModel> Sign(TModel model)
    {
        var failedModel = new Valid<TModel>(default, new ValidationResult(new[] { new ValidationFailure("", "") }));
        return failedModel;
    }
}