using System.Linq;
using Xunit;
using NBA_Lib.Model;

namespace NBA_Test.Models
{
    public class TeamData_Test
    {
        [Theory]
        [InlineData(0, "1949-50", "2018-19")]
        [InlineData(222,"1949-50", "1949-50")]
        [InlineData(333,"1960-61","1966-67")]
        public void GetActiveSeasons_ShouldWork(int teamId, string max, string min)
        {
            var teams = new MockData().FranchiseData.ExtractTeamData();

            var actual = (teamId == 0) ? TeamData.GetActiveSeasons(teams) 
                : TeamData.GetActiveSeasons(teams.Where(t => t.TeamID == teamId).ToList());

            Assert.Equal(min, actual[0]);
            Assert.Equal(max, actual[actual.Count - 1]);
        }
    }
}
