using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NBA_MK.ValueConverters;

namespace NBA_Test.Converters
{
    public class StreakColorConverter_Test
    {
        [Theory]
        [InlineData(0)]
        public void Convert_ShouldWork_PassValueEqualZero(object val)
        {
            var actual = new StreakColorConverter().Convert(val, null, null, null);

            var expected = System.Windows.Media.Brushes.Black;

            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(67)]
        public void Convert_ShouldWork_PassValueGreaterThanZero(object val)
        {
            var actual = new StreakColorConverter().Convert(val, null, null, null);

            var expected = System.Windows.Media.Brushes.DarkGreen;

            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-66)]
        public void Convert_ShouldWork_PassValueLessThanZero(object val)
        {
            var actual = new StreakColorConverter().Convert(val, null, null, null);

            var expected = System.Windows.Media.Brushes.DarkRed;

            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null)]
        public void Convert_ShouldWork_PassNull(object val)
        {
            var actual = new StreakColorConverter().Convert(val, null, null, null);

            var expected = System.Windows.Media.Brushes.Black;

            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(2.22)]
        [InlineData("Hi")]
        public void Convert_ShouldWork_PassInvalidData(object val)
        {
            var actual = new StreakColorConverter().Convert(val, null, null, null);

            var expected = System.Windows.Media.Brushes.Black;

            Assert.Equal(actual, expected);
        }

    }
}
