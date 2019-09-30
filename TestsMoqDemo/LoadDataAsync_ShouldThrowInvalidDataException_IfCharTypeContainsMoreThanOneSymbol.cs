using Autofac.Extras.Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TxtFileDataAccess;
using Xunit;

namespace TestsMoqDemo
{
    public partial class LoadDataAsyncTests
    {
        [Fact]
        public async Task LoadDataAsync_ShouldThrowInvalidDataException_IfCharTypeContainsMoreThanOneSymbol()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ITxtFileReader>()
                    .Setup(reader => reader.ReadAllLinesAsync())
                    .Returns(Task.FromResult((IEnumerable<string>)new[] { "Symbol=123data!;Data=s;" }));

                var accessor = mock.Create<TxtFileDataAccess<SampleClassWithTwoChars>>();

                await Assert.ThrowsAsync<InvalidDataException>(() => accessor.LoadDataAsync());
            }
        }
    }
}
