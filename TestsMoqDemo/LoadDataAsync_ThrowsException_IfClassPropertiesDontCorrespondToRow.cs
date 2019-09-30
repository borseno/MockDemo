using Autofac.Extras.Moq;
using MoqDemoPractice;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestsMoqDemo
{
    public partial class LoadDataAsyncTests
    {
        private class MyIntStringClass
        {
            public string Value { get; set; }
            public int Boolean { get; set; }
        }

        [Fact]
        public async Task LoadDataAsync_ThrowsException_IfClassPropertiesDontCorrespondToRow()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ITxtFileReader>()
                    .Setup(reader => reader.ReadAllLinesAsync())
                    .Returns(GetSampleRowsAsync());

                var accessor = mock.Create<TxtFileDataAccess<MyIntStringClass>>();

                await Assert.ThrowsAsync<InvalidOperationException>(() => accessor.LoadDataAsync());
            }
        }
    }
}
