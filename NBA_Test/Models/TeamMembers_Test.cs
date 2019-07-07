using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NBA_Lib.Model;

namespace NBA_Test.Models
{
    public class TeamMembers_Test
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("F", "Forward")]
        [InlineData("C", "Center")]
        [InlineData("G", "Guard")]
        [InlineData("F-G", "Forward-Guard")]
        [InlineData("F-C", "Forward-Center")]
        [InlineData("G-C", "Guard-Center")]
        [InlineData("Coach", "Coach")]
        public void ConvertPosition_ShouldWork(string position, string expected)
        {
            var actual = new TeamMembers().ConvertPosition(position);

            Assert.Equal(actual, expected);
        }

    }
}
