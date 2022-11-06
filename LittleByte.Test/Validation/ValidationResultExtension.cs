﻿using System.Linq;
using FluentValidation.Results;
using NUnit.Framework;

namespace LittleByte.Test.Validation;

public static class ValidationResultExtension
{
    public static void AssertFailure(this ValidationResult @this, string propertyName)
    {
        Assert.IsFalse(@this.IsValid, "Expected failed validation");
        var error = @this.Errors.Single(e => e.PropertyName == propertyName);
        Assert.IsNotNull(error, $"No validation failure for property '{propertyName}'");
    }
}