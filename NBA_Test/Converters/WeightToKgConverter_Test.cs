using Xunit;
using NBA_MK.ValueConverters;

namespace NBA_Test.Converters
{
    public class WeightToKgConverter_Test
    {
        [Theory]
        [InlineData(0, "0 kg")]
        [InlineData(100, "45 kg")]
        [InlineData(300, "136 kg")]
        public void Convert_ShouldWork_ValidData(object input, object expected)
        {
            var actual = new WeightToKgConverter().Convert(input, null, null, null);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, null)]
        public void Convert_ShouldWork_InvalidData(object input, object expected)
        {
            var actual = new WeightToKgConverter().Convert(input, null, null, null);

            Assert.Equal(expected, actual);
        }

    }
}
