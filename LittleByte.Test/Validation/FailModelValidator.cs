using FluentValidation.Results;
using LittleByte.Common.Validation;

namespace LittleByte.Test.Validation;

public class FailModelValidator<TModel> : ModelValidator<TModel>
{
    public override Valid<TModel> Sign(TModel model)
    {
        var failedModel = new Valid<TModel>(default, new ValidationResult(new[] {new ValidationFailure("", "")}));
        return failedModel;
    }
}