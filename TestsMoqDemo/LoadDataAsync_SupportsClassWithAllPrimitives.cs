using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit;

namespace TestsMoqDemo
{
    public partial class LoadDataAsyncTests
    { 
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
        }

        [Fact]
        public async Task LoadDataAsync_SupportsClassWithAllPrimitives()
        {

        }
    }
}
