using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NBA_MK.ValueConverters;

namespace NBA_Test.Converters
{
    public class HeightToCmConverter_Test
    {
        [Theory]
        [InlineData(6,"182 cm")]
        [InlineData("6-1","185 cm")]
        [InlineData("6-11","210 cm")]
        [InlineData(0,"0 cm")]
        public void Convert_ShouldWork_ValidData(object input, object expected)
        {
            var actual = new HeightToCmConverter().Convert(input, null, null, null);

            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        public void Convert_ShouldWork_InvalidData(object input, object expected)
        {
            var actual = new HeightToCmConverter().Convert(input, null, null, null);

            Assert.Equal(expected, actual);
        }
    }
}
