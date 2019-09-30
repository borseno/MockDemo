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
        [Fact]
        public async Task LoadDataAsync_SupportsClassWithAllPrimitives()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ITxtFileReader>()
                    .Setup(reader => reader.ReadAllLinesAsync())
                    .Returns(GetSamplePrimitiveInstancesRowsAsync());

                var accessor = mock.Create<TxtFileDataAccess<SampleClassWithAllPrimitives>>();

                var expected = GetSampleClassWithAllPrimitivesInstances().ToList();
                var actual = (await accessor.LoadDataAsync())
                    .ToList();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].Value1, actual[i].Value1);
                    Assert.Equal(expected[i].Value2, actual[i].Value2);
                    Assert.Equal(expected[i].Value3, actual[i].Value3);
                    Assert.Equal(expected[i].Value4, actual[i].Value4);
                    Assert.Equal(expected[i].Value5, actual[i].Value5);
                    Assert.Equal(expected[i].Value6, actual[i].Value6);
                    Assert.Equal(expected[i].Value7, actual[i].Value7);
                    Assert.Equal(expected[i].Value8, actual[i].Value8);
                    Assert.Equal(expected[i].Value9, actual[i].Value9);
                    Assert.Equal(expected[i].Value10, actual[i].Value10);
                    Assert.Equal(expected[i].Value11, actual[i].Value11);
                    Assert.Equal(expected[i].Value12, actual[i].Value12);
                    Assert.Equal(expected[i].Value13, actual[i].Value13);
                    Assert.Equal(expected[i].Value14, actual[i].Value14);
                }
            }
        }

        private class SampleClassWithAllPrimitives
        {
            public int Value1 { get; set; }
            public short Value2 { get; set; }
            public long Value3 { get; set; }
            public byte Value4 { get; set; }
            public float Value5 { get; set; }
            public double Value6 { get; set; }
            public decimal Value7 { get; set; }
            public char Value8 { get; set; }
            public uint Value9 { get; set; }
            public sbyte Value10 { get; set; }
            public ushort Value11 { get; set; }
            public bool Value12 { get; set; }
            public string Value13 { get; set; }
            public ulong Value14 { get; set; }

            public override string ToString()
            {
                return
                    $"Value1={Value1};" +
                    $"Value2={Value2};" +
                    $"Value3={Value3};" +
                    $"Value4={Value4};" +
                    $"Value5={Value5};" +
                    $"Value6={Value6};" +
                    $"Value7={Value7};" +
                    $"Value8={Value8};" +
                    $"Value9={Value9};" +
                    $"Value10={Value10};" +
                    $"Value11={Value11};" +
                    $"Value12={Value12};" +
                    $"Value13={Value13};" +
                    $"Value14={Value14};";
            }
        }

        private IEnumerable<SampleClassWithAllPrimitives> GetSampleClassWithAllPrimitivesInstances()
        {
            return new[]
            {
                new SampleClassWithAllPrimitives
                {
                    Value13 = "Whatsup! 123",
                    Value12 = true,
                    Value2 = 34,
                    Value6 = 45.4634D,
                    Value7 = 45.453535M
                },
                new SampleClassWithAllPrimitives
                {
                    Value13 = "Yo, second instance// 765432101234567",
                    Value12 = false,
                    Value2 = 12111,
                    Value6 = 45.0024634D,
                    Value7 = 45.001453535M
                }
            };
        }

        private IEnumerable<string> GetSamplePrimitiveInstancesRows() =>
            GetSampleClassWithAllPrimitivesInstances()
            .Select(i => i.ToString())
            .ToArray();

        private Task<IEnumerable<string>> GetSamplePrimitiveInstancesRowsAsync() => 
            Task.FromResult(GetSamplePrimitiveInstancesRows());
    }
}
