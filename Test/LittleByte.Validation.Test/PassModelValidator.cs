using FluentValidation.Results;

namespace LittleByte.Validation.Test;

public class PassModelValidator<TModel> : ModelValidator<TModel>
{
    public override Valid<TModel> Sign(TModel model)
    {
        var signedModel = new Valid<TModel>(model, new ValidationResult());
        return signedModel;
    }
}