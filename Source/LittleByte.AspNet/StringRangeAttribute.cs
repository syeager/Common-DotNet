using System.ComponentModel.DataAnnotations;

namespace LittleByte.AspNet;

public class StringRangeAttribute : StringLengthAttribute
{
    public StringRangeAttribute(int minLength, int maxLength)
        : base(maxLength)
    {
        MinimumLength = minLength;
    }
}