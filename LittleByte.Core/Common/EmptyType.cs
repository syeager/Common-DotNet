using System.Diagnostics.CodeAnalysis;

namespace LittleByte.Core.Common
{
    /// <summary>
    /// Only used for shortening lines when a generic type is necessary for the compiler but the code doesn't require it (eg: nameof, accessing static members, unit tests).
    /// </summary>
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class X { }
}
