using Autofac.Extras.Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TxtFileDataAccess;
using Xunit;

namespace TestsMoqDemo
{
    public partial class LoadDataAsyncTests
    {
        private class SampleClassWithTwoChars
        {
            public char Symbol { get; set; }
            public char Data { get; set; }
        }

        [Fact]
        public async Task LoadDataAsync_ShouldProduceDefaultChar_WithEmptyValue()
        {
            // Arrange
            ICollection<SampleClassWithTwoChars> expected = new List<SampleClassWithTwoChars>
                {
                    new SampleClassWithTwoChars()
                };

            // Act
            ICollection<SampleClassWithTwoChars> actual;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ITxtFileReader>()
                    .Setup(reader => reader.ReadAllLinesAsync())
                    .Returns(Task.FromResult((IEnumerable<string>)new[] { "Symbol=;Data=;" }));

                var accessor = mock.Create<TxtFileDataAccess<SampleClassWithTwoChars>>();

                actual = await accessor.LoadDataAsync();
            }

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected.ElementAt(i).Data, actual.ElementAt(i).Data);
                Assert.Equal(expected.ElementAt(i).Symbol, actual.ElementAt(i).Symbol);
            }
        }
    }
}
