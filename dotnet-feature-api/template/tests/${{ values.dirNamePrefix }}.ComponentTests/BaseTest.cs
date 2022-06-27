namespace ${{ values.namespacePrefix }}.ComponentTests;

public class BaseTest : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    public readonly TestClientBuilder TestClientBuilder;
    public readonly string BaseUrl;

    public BaseTest(WebApplicationFactory<Program> factory, string baseUrl = "/api/v1")
    {
        TestClientBuilder = new TestClientBuilder(factory);
        BaseUrl = baseUrl;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this); // more details here https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1816 
    }
}
