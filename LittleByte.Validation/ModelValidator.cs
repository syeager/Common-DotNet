using FluentValidation;

namespace LittleByte.Validation;

public interface IModelValidator<TModel>
{
    Valid<TModel> Sign(TModel model);
    void SignOrThrow(TModel model);
}

public class ModelValidator<TModel> : AbstractValidator<TModel>, IModelValidator<TModel>
{
    public virtual Valid<TModel> Sign(TModel model)
    {
        var result = Validate(model);
        var signedModel = new Valid<TModel>(model, result);
        return signedModel;
    }

    public void SignOrThrow(TModel model)
    {
        var result = Sign(model);
        if (!result.IsSuccess)
        {
            throw new ValidationException(
                $"Validation failure for '{typeof(TModel)}' using validator '{GetType()}.",
                result.Validation.Errors);
        }
    }
}