namespace ${{ values.namespacePrefix }}.ComponentTests;

public class TestClientBuilder
{
    private readonly WebApplicationFactory<Program> _factory;
    public TestClientBuilder(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    public TestClient Build(Action<IServiceCollection> configureTestServices)
    {
        var wireMockServer = WireMockServer.Start();
        var port = wireMockServer.Ports.First();
        var url = $"http://localhost:{port}/";

        var httpClient = _factory
            .WithWebHostBuilder(
                builder =>
                {
                    builder.UseEnvironment("testserver");
                    builder.ConfigureTestServices(services =>
                    {
                        configureTestServices(services);
                        services.AddHttpClient("valueClient", c => { c.BaseAddress = new Uri(url); });
                    });
                })
            .CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                    HandleCookies = false
                });
        return new TestClient(wireMockServer, httpClient);
    }
}