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

    public class FailModelValidator<TModel> : ModelValidator<TModel>
        where TModel : class
    {
        public override Valid<TModel> Sign(TModel model)
        {
            var failedModel = new Valid<TModel>(null, new ValidationResult(new[] {new ValidationFailure("", "")}));
            return failedModel;
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
