using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace LittleByte.Logging.Test.TestUtilities
{
    // TODO: Move to core extensions lib.
    public static class ICallExtension
    {
        public static T? Arg<T>(this ICall @this)
        {
            var arg = @this.GetArguments().FirstOrDefault(a => a is T);
            if(arg != null)
            {
                return (T)arg;
            }

            return default;
        }
    }

    // TODO: Move to core extensions lib.
    public static class ILoggerExtension
    {
        public static void AssertMessage(this ILogger @this, LogLevel level, string message, int count = 1)
        {
            var found = @this.ReceivedCalls()
                .Count(ci => ci.Arg<LogLevel>() == level && ci.GetArguments()[2]?.ToString() == message);

            LogAssert(found, count, level, "message", message);
        }

        public static void AssertMessage(this ILogger @this, LogLevel level, Predicate<string?> predicate, int count = 1)
        {
            var found = @this.ReceivedCalls()
                .Count(ci => ci.Arg<LogLevel>() == level && predicate(ci.GetArguments()[2]?.ToString()));

            LogAssert(found, count, level, "message", "_predicate_");
        }

        public static void AssertLevel(this ILogger @this, LogLevel level, int count = 1)
        {
            var found = @this.ReceivedCalls()
                .Count(ci => ci.Arg<LogLevel>() == level);

            LogAssert(found, count, level, "level", level);
        }

        public static void AssertParamNames(this ILogger @this, string name, LogLevel level, int count = 1)
        {
            var found = IterateParams(@this, name, level, pair => pair.Key == name);

            LogAssert(found.Count, count, level, "param name", name);
        }

        public static void AssertParamValues(this ILogger @this, object value, LogLevel level, int count = 1)
        {
            var found = IterateParams(@this, value, level, pair => pair.Value == value);

            LogAssert(found.Count, count, level, "param value", value);
        }

        private static IReadOnlyCollection<(string name, object value)> IterateParams(ILogger logger, object value, LogLevel level, Func<KeyValuePair<string, object>, bool> predicate)
        {
            var parameters = logger.ReceivedCalls()
                .Where(ci => ci.Arg<LogLevel>() == level)
                .Select(ci => ci.Arg<IReadOnlyList<KeyValuePair<string, object>>>()).ToImmutableArray();

            var found = parameters
                .SelectMany(p => p!.Where(predicate).Select(kvp => (kvp.Key, kvp.Value)))
                .ToImmutableArray();

            return found;
        }

        private static void LogAssert(int found, int count, LogLevel level, string searchProperty, object searchValue)
        {
            if(found != count)
            {
                var failMessage = $"Was looking for {count} but found {found} of log(s) at level {level} with {searchProperty} of '{searchValue}'.";
                Assert.Fail(failMessage);
            }
        }
    }
}