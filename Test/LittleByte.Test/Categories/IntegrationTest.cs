using LittleByte.Serilog;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace LittleByte.Test.Categories;

[Category("integration")]
public abstract class IntegrationTest : TestCategory
{
    protected ServiceProvider services = null!;

    protected abstract void SetupInternal(IServiceCollection serviceCollection);

    [SetUp]
    public virtual void SetUp()
    {
        var serviceCollection = new ServiceCollection();

        LogsConfiguration.InitLogging();
        //AddTokens(serviceCollection);
        SetupInternal(serviceCollection);

        services = serviceCollection.BuildServiceProvider();
    }

    [TearDown]
    public virtual void TearDown()
    {
        services.Dispose();
    }

    // TODO: Where does this go?
    //private static void AddTokens(IServiceCollection serviceCollection)
    //{
    //    serviceCollection
    //        .AddTransient<ITokenGenerator, NullTokenGenerator>()
    //        .AddTransient<SecurityTokenHandler, JwtSecurityTokenHandler>();
    //}

    protected TService GetService<TService>()
        where TService : notnull
    {
        return services.GetRequiredService<TService>();
    }
}