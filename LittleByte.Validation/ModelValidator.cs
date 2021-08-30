using FluentValidation;
using FluentValidation.Results;

namespace LittleByte.Validation
{
    public interface IModelValidator<TModel>
    {
        ValidModel<TModel> Sign(TModel model);
    }

    public class ModelValidator<TModel> : AbstractValidator<TModel>, IModelValidator<TModel>
    {
        public virtual ValidModel<TModel> Sign(TModel model)
        {
            var result = Validate(model);
            var signedModel = new ValidModel<TModel>(model, result);
            return signedModel;
        }
    }

    public class SuccessModelValidator<TModel> : ModelValidator<TModel>
    {
        public override ValidModel<TModel> Sign(TModel model)
        {
            var signedModel = new ValidModel<TModel>(model, new ValidationResult());
            return signedModel;
        }
    }
}