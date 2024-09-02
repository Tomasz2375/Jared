namespace Jared.Api.Integration.Tests;

[Collection("TestCollection")]
public abstract class BaseIntegrationTest : IDisposable
{
    private readonly JaredWebApplicationFactory factory;
    protected abstract string URL { get; }
    protected HttpClient Client { get; }

    protected BaseIntegrationTest(JaredWebApplicationFactory factory)
    {
        this.factory = factory;
        Client = factory.CreateClient();
        Client.BaseAddress = new Uri("https://localhost:7050/api/");
    }

    public void Dispose()
    {
        factory.RestoreSnapshot();
        GC.SuppressFinalize(this);
    }
}
