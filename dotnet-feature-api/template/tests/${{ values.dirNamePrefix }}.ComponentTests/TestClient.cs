namespace ${{ values.namespacePrefix }}.ComponentTests;

public class TestClient : IDisposable
{
    public HttpClient HttpClient { get; }

    public WireMockServer WireMockServer { get; }

    public TestClient(WireMockServer wireMockServer, HttpClient httpClient)
    {
        HttpClient = httpClient;
        WireMockServer = wireMockServer;
    }

    public void Dispose()
    {
        HttpClient.Dispose();
        WireMockServer.Dispose();
        GC.SuppressFinalize(this);
    }
}
