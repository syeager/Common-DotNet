using FluentValidation.Results;
using LittleByte.Common.Validation;

namespace LittleByte.Test.Validation;

public class PassModelValidator<TModel> : ModelValidator<TModel>
{
    public override Valid<TModel> Sign(TModel model)
    {
        var signedModel = new Valid<TModel>(model, new ValidationResult());
        return signedModel;
    }
}