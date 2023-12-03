using LittleByte.Serilog;

namespace LittleByte.Test.Categories;

public abstract class TestCategory
{
    protected TestCategory()
    {
        LogsConfiguration.InitLogging();
    }
}