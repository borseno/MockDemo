using Autofac.Extras.Moq;
using System;
using System.Threading.Tasks;
using TxtFileDataAccess;
using Xunit;

namespace TestsMoqDemo
{
    public partial class LoadDataAsyncTests
    {
        private class ClassWithNonPrimitive
        {
            public ClassWithNonPrimitive Property { get; set; } 
        }

        [Fact]
        public async Task LoadDataAsync_ShouldThrowNotSupportedException_IfClassContainsNonPrimitiveType()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ITxtFileReader>()
                    .Setup(reader => reader.ReadAllLinesAsync())
                    .Returns(GetSampleRowsAsync());

                var accessor = mock.Create<TxtFileDataAccess<ClassWithNonPrimitive>>();

                await Assert.ThrowsAsync<NotSupportedException>(() => accessor.LoadDataAsync());
            }
        }
    }
}
