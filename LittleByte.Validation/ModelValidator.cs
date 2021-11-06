using FluentValidation;
using FluentValidation.Results;

namespace LittleByte.Validation
{
    public interface IModelValidator<TModel>
    {
        Valid<TModel> Sign(TModel model);
    }

    public class ModelValidator<TModel> : AbstractValidator<TModel>, IModelValidator<TModel>
    {
        public virtual Valid<TModel> Sign(TModel model)
        {
            var result = Validate(model);
            var signedModel = new Valid<TModel>(model, result);
            return signedModel;
        }
    }

    public class SuccessModelValidator<TModel> : ModelValidator<TModel>
    {
        public override Valid<TModel> Sign(TModel model)
        {
            var signedModel = new Valid<TModel>(model, new ValidationResult());
            return signedModel;
        }
    }
}