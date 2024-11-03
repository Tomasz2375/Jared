namespace Jared.Api.Integration.Tests;

public abstract class BaseIntegrationTest : IClassFixture<JaredWebApplicationFactory>
{
    protected abstract string URL { get; }
    protected HttpClient Client { get; }

    protected BaseIntegrationTest(JaredWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        Client.BaseAddress = new Uri("https://localhost:7050/api/");
    }
}
