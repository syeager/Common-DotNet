using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace LittleByte.Test.Categories;

[Category("integration")]
public abstract class IntegrationTest : TestCategory
{
    protected ServiceProvider services = null!;

    protected abstract void SetupInternal(IServiceCollection serviceCollection, IConfiguration configuration);

    protected virtual void ConfigureInternal(ConfigurationBuilder builder) { }

    [SetUp]
    public virtual void SetUp()
    {
        var configuration = Configure();
        var serviceCollection = new ServiceCollection();
        SetupInternal(serviceCollection, configuration);

        services = serviceCollection.BuildServiceProvider();
    }

    [TearDown]
    public virtual void TearDown()
    {
        services.Dispose();
    }

    protected TService GetService<TService>()
        where TService : notnull
    {
        return services.GetRequiredService<TService>();
    }

    private IConfiguration Configure()
    {
        var builder = new ConfigurationBuilder();
        ConfigureInternal(builder);
        return builder.Build();
    }
}