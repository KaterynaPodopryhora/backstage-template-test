using ${{ values.namespacePrefix }}.ComponentTests.Stubs;

namespace ${{ values.namespacePrefix }}.ComponentTests.Api;

public class FindByIdTests : BaseTest
{
    private readonly TestClient _testClient;

    public FindByIdTests(WebApplicationFactory<Program> factory) : base(factory)
    {
         _testClient = TestClientBuilder.Build(services =>
        {
            services.AddSingleton(new MeasurementRepositoryStub().Stub);
        });
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task When_IdUnknown_Returns_NoContent(string id)
    {
        // Arrange
        var client = _testClient.HttpClient;

        // Act
        var response = await client.GetAsync($"{BaseUrl}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Null(response.Content.Headers.ContentType);
        Assert.True(response.Content.Headers.ContentLength == 0);
    }

    [Theory]
    [InlineData("not-a-valid-guid")]
    public async Task When_IdMalformed_Returns_NotFound(string id)
    {
        // Arrange
        var client = _testClient.HttpClient;

        // Act
        var response = await client.GetAsync($"{BaseUrl}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Null(response.Content.Headers.ContentType);
        Assert.True(response.Content.Headers.ContentLength == 0);
    }
}
