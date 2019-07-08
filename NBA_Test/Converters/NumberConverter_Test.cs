using Xunit;
using NBA_MK.ValueConverters;

namespace NBA_Test.Converters
{
    public class NumberConverter_Test
    {
        [Theory]
        [InlineData(13, "13")]
        [InlineData("12-23", "12, 23")]
        [InlineData("12-13-14", "12, 13, 14")]
        public void Convert_ShouldWork_ValidData(object input, object expected)
        {
            var actual = new NumberConverter().Convert(input, null, null, null);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("hello", "hello")]
        [InlineData(22.2, "22,2")]
        [InlineData(null, null)]
        public void Convert_ShouldWork_InvalidData(object input, object expected)
        {
            var actual = new NumberConverter().Convert(input, null, null, null);

            Assert.Equal(expected, actual);
        }
    }
}
