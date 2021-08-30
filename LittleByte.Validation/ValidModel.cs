﻿using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;

namespace LittleByte.Validation
{
    public readonly struct ValidModel<T>
    {
        private T? Model { get; }
        public ValidationResult Validation { get; }

        [MemberNotNullWhen(true, nameof(Model))]
        public bool IsSuccess => Model != null && Validation.IsValid;

        internal ValidModel(T? model, ValidationResult validation)
        {
            Model = model;
            Validation = validation;
        }

        /// <exception cref="ValidationException"/>
        public T GetModelOrThrow()
        {
            if(!IsSuccess)
            {
                throw new ValidationException($"Validation failure for '{typeof(T)}`.", Validation.Errors);
            }

            return Model;
        }
    }
}