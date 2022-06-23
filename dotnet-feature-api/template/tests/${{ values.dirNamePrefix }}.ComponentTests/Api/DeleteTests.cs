namespace ${{ values.namespacePrefix }}.ComponentTests.Api;

public class DeleteTests : BaseTest
{
    private readonly TestClient _testClient;

    public DeleteTests(WebApplicationFactory<Program> factory) : base(factory)
    {
        _testClient = TestClientBuilder.Build(services =>
        {
            services.AddSingleton(new MeasurementRepositoryStub().Stub);
        });
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    public async Task When_IdUnknown_Returns_NoContent(string id)
    {
        // Arrange
        var client = TestClient.HttpClient;

        // Act
        var response = await client.DeleteAsync($"{BaseUrl}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Null(response.Content.Headers.ContentType);
        Assert.True(response.Content.Headers.ContentLength == 0);
    }
}
