using LittleByte.Serilog;
using Serilog;

namespace LittleByte.Test.Categories;

public abstract class TestCategory
{
    protected TestCategory()
    {
        SerilogConfiguration.UseSerilog(config =>
            config
                .Enrich.FromLogContext()
                .Enrich.With<RemoveSourceContextEnricher>()
                .WriteTo.Console(outputTemplate: SerilogConfiguration.DefaultTemplate));
    }
}