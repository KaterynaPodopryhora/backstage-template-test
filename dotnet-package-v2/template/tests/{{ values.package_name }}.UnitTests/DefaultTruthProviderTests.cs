using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace {{values.package_name}}.UnitTests
{
    public class DefaultTruthProviderTests
    {
        private DefaultTruthProvider _sut;

        public DefaultTruthProviderTests()
        {
            _sut = new DefaultTruthProvider();
        }

        [Fact]
        public async Task WhenArgumentIsNull_ThenArgumentNullExceptionIsThrown()
        {
            //Act
            Func<Task> action = () => _sut.GetTruth(null);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData("value")]
        [InlineData(1.0)]
        public async Task Always_ReturnsTheTruth(object value)
        {
            //Act
            var result = await _sut.GetTruth(value);

            //Assert
            result.Should().Be(42);
        }
    }
}
