using LittleByte.Common.Logging;
using NUnit.Framework;
using Serilog;
using Log = Serilog.Log;

namespace LittleByte.Test.Categories;

[Category("unit")]
public abstract class UnitTest
{
    protected UnitTest()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.With<RemovePropertiesEnricher>()
            .WriteTo.Console(
                outputTemplate: Logs.DefaultTemplate)
            .CreateLogger();
    }
}