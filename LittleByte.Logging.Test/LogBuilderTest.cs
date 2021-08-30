using LittleByte.Logging.Test.TestUtilities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace LittleByte.Logging.Test
{
    public class ExampleObj
    {
        public string Name { get; } = "name";
        public int Value { get; } = 1;
    }

    /*
     * happy
     * Over max size
     * Log calls Dispose
     */
    public class LogBuilderTest
    {
        private const string FormatTemplate = "{1}, {2}";
        private LogBuilder testObj = null!;
        private ILogger logger = null!;
        private ExampleObj exampleObj = null!;

        [SetUp]
        public void SetUp()
        {
            logger = Substitute.For<ILogger>();
            exampleObj = new ExampleObj();

            testObj = LogBuilder.Create(FormatTemplate, logger);
        }

        [Test]
        public void Add_NotMemberExpression_LogWarning()
        {
            testObj.Add(() => "");

            logger.AssertLevel(LogLevel.Warning);
        }

        [Test]
        public void Add_AlreadyDisposed_LogWarning()
        {
            testObj.Dispose();

            testObj.Add(() => exampleObj.Name);
        }

        [Test]
        public void LogAndDispose_FormatException_LogWarning()
        {
            testObj.LogAndDispose();

            logger.AssertLevel(LogLevel.Warning);
            logger.AssertLevel(LogLevel.Information, 0);
        }

        [Test]
        public void LogAndDispose_AlreadyDisposed_LogWarning()
        {
            testObj.Dispose();

            testObj.LogAndDispose();

            logger.AssertLevel(LogLevel.Warning);
            logger.AssertLevel(LogLevel.Information, 0);
        }

        [Test]
        public void Add_OverMax_NoWarn()
        {
            testObj = LogBuilder.Create(FormatTemplate, logger, 1);
            testObj.Add(() => exampleObj.Name);

            testObj.Add(() => exampleObj.Name);

            logger.AssertLevel(LogLevel.Warning, 0);
        }

        [Test]
        public void LogAndDispose_OverMax_CapAtMaxAndLogWarning()
        {
            const int maxSize = 1;
            testObj = LogBuilder.Create(FormatTemplate, logger, maxSize);
            testObj.Add(() => exampleObj.Name);
            testObj.Add(() => exampleObj.Value);

            testObj.LogAndDispose();

            logger.AssertMessage(LogLevel.Information, m => m!.Contains(testObj.GetParamName(() => exampleObj.Name)), 0);
            logger.AssertLevel(LogLevel.Warning, 2);
        }
    }
}