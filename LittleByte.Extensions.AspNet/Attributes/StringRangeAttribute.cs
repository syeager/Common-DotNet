﻿using System.ComponentModel.DataAnnotations;

namespace LittleByte.Extensions.AspNet.Attributes;

public class StringRangeAttribute : StringLengthAttribute
{
    public StringRangeAttribute(int minLength, int maxLength)
        : base(maxLength)
    {
        MinimumLength = minLength;
    }
}