using LittleByte.Common.Logging;
using Serilog;
using Log = Serilog.Log;

namespace LittleByte.Test.Categories;

public abstract class TestCategory
{
    protected TestCategory()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.With<RemovePropertiesEnricher>()
            .WriteTo.Console(
                outputTemplate: Logs.DefaultTemplate)
            .CreateLogger();
    }
}