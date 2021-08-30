namespace LittleByte.Core.Extensions
{
    public static class UintExtension
    {
        public static uint Increment(this uint @this)
        {
            return @this == uint.MaxValue ? uint.MaxValue : @this + 1;
        }

        public static uint Decrement(this uint @this)
        {
            return @this == 0 ? 0 : @this - 1;
        }
    }
}