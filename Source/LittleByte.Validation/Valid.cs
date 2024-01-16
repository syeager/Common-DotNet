using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;

namespace LittleByte.Validation;

public readonly struct Valid<T>
{
    private T? Model { get; }
    public ValidationResult Validation { get; }

    [MemberNotNullWhen(true, nameof(Model))]
    public bool IsSuccess => Validation.IsValid;

    internal Valid(T? model, ValidationResult validation)
    {
        Model = model;
        Validation = validation;
    }

    /// <exception cref="ValidationException" />
    public T GetModelOrThrow()
    {
        ThrowIfInvalid();

        return Model!;
    }

    /// <exception cref="ValidationException" />
    public void ThrowIfInvalid()
    {
        if (!IsSuccess)
        {
            throw new ValidationException($"Validation failure for '{typeof(T)}'.", Validation.Errors);
        }
    }
}