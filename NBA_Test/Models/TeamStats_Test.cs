using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NBA_Lib.Model;

namespace NBA_Test.Models
{
    public class TeamStats_Test
    {
        [Fact]
        public void CorrectStanding_ShouldWork_TeamsWithStanding()
        {
            var teams = new MockData().LeagueStandings.ExtractTeamStats("2010-11");

            var tmp = TeamStats.CorrectStanding(teams.Where(t => t.Standing != 0));

            List<TeamStats> actual = tmp.ToList();

            Assert.Equal(2, actual[0].Standing);
            Assert.Equal(3, actual[1].Standing);
        }
        [Fact]
        public void CorrectStanding_ShouldWork_TeamsWithoutStanding()
        {
            var teams = new MockData().LeagueStandings.ExtractTeamStats("2010-11");

            var tmp = TeamStats.CorrectStanding(teams.Where(t => t.Standing == 0));

            List<TeamStats> actual = tmp.ToList();

            Assert.Equal(1, actual[0].Standing);
            Assert.Equal(2, actual[1].Standing);
        }
        [Fact]
        public void CorrectStanding_ShouldWork_TeamsWithAndWithoutStanding()
        {
            var teams = new MockData().LeagueStandings.ExtractTeamStats("2010-11");

            var actual = TeamStats.CorrectStanding(teams).ToList();

            Assert.Contains(actual, p => p.Standing == 0);
            Assert.Contains(actual, p => p.Standing == 2);
            Assert.Contains(actual, p => p.Standing == 3);
        }
    }
}
