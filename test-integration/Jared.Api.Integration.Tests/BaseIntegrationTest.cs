using Jared.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Jared.Api.Integration.Tests;

[Collection("TestCollection")]
public abstract class BaseIntegrationTest : IDisposable
{
    private readonly IServiceScope scope;

    protected readonly IDataContext DataContext;
    protected HttpClient Client { get; }

    protected BaseIntegrationTest(JaredWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        Client.BaseAddress = new Uri("https://localhost:7050/api/");

        scope = factory.Services.CreateScope();
        DataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
