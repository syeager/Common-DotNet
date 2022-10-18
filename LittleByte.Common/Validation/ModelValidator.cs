using FluentValidation;
using FluentValidation.Results;

namespace LittleByte.Common.Validation
{
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
            if(!result.IsSuccess)
            {
                throw new ValidationException($"Validation failure for '{typeof(TModel)}' using validator '{GetType()}.", result.Validation.Errors);
            }
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
