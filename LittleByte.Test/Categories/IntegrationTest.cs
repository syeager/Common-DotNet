using System.IdentityModel.Tokens.Jwt;
using LittleByte.Common.Identity.Services;
using LittleByte.Common.Logging;
using LittleByte.Common.Logging.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using Serilog;

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

        AddLogging(serviceCollection);
        AddTokens(serviceCollection);
        SetupInternal(serviceCollection);

        services = serviceCollection.BuildServiceProvider();
    }

    [TearDown]
    public virtual void TearDown()
    {
        services.Dispose();
    }

    private static void AddLogging(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IDiagnosticContext, NullDiagnosticContext>()
            .AddTransient(typeof(ILogger<>), typeof(NullLogger<>))
            .AddLogs();
    }

    private static void AddTokens(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<ITokenGenerator, NullTokenGenerator>()
            .AddTransient<SecurityTokenHandler, JwtSecurityTokenHandler>();
    }
}