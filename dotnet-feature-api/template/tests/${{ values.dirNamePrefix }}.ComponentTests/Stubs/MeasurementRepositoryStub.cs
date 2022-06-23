namespace ${{ values.namespacePrefix }}.ComponentTests.Stubs;
public class MeasurementRepositoryStub
{
    private readonly Mock<IMeasurementRepository> _stubber = new();

    public IMeasurementRepository Stub => _stubber.Object;

    public MeasurementRepositoryStub
    {
    }

    public void ConfigureCreateAsync(IMeasurement result)
    {
        _stubber
            .Setup(o => o.CreateAsync(It.IsAny<IMeasurement>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => result);
    }

    public void ConfigureFindByIdAsync(IMeasurement result)
    {
        _stubber
            .Setup(o => o.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => result);
    }

    public void ConfigureDeleteAsync(bool result)
    {
        _stubber
            .Setup(o => o.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => result);
    }
}
