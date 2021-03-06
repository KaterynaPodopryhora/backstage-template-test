namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public interface ICosmosDbClientService
{
    public CosmosDbOptions Options { get; }
    Task<Database> GetDatabaseAsync(string databaseId);
    Task<Container> GetContainerAsync(Database database, string containerId, ContainerProperties containerProperties);
}

public class CosmosDbClientService : ICosmosDbClientService
{
    private readonly CosmosClient _client;
    private readonly CosmosDbOptions _options;

    public CosmosDbOptions Options => _options;

    public CosmosDbClientService(IOptions<CosmosDbOptions> optionsAccessor)
    {
        _options = optionsAccessor.Value;

        _client = new CosmosClient(_options.ConnectionString);
    }

    public async Task<Database> GetDatabaseAsync(string databaseId)
    {
        if (_options.CreateDatabaseAndContainersIfNotExists)
        {
            return await _client.CreateDatabaseIfNotExistsAsync(databaseId);
        }

        return _client.GetDatabase(databaseId);
    }

    public async Task<Container> GetContainerAsync(Database database, string containerId, ContainerProperties containerProperties)
    {
        if (_options.CreateDatabaseAndContainersIfNotExists)
        {
            return await database.CreateContainerIfNotExistsAsync(containerProperties);
        }

        return database.GetContainer(containerId);
    }
}
