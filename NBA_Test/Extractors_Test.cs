using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NBA_Lib.JsonReader.JsonRoots;

namespace NBA_Test
{
    public class Extractors_Test
    {
        #region ExtractTeamStats

        [Fact]
        public void ExtractTeamStats_ShouldWork_ValidDataPre1970()
        {
            var league = new MockData().LeagueStandings;

            var actual = league.ExtractTeamStats("1960-61");

            Assert.Equal("West", actual[0].Conference);
        }

        [Fact]
        public void ExtractTeamStats_ShouldWork_ValidDataPost1970()
        {
            var league = new MockData().LeagueStandings;

            var actual = league.ExtractTeamStats("2000-01");

            Assert.Equal(111, actual[0].TeamID);
            Assert.Equal("Chicago Bulls", actual[0].TeamName);
            Assert.Equal("East", actual[0].Conference);
            Assert.Equal(1, actual[0].Standing);
            Assert.Equal(9, actual[0].Wins);
            Assert.Equal(1, actual[0].Losses);
            Assert.Equal(0.9, actual[0].Ratio);
            Assert.Equal(5, actual[0].Streak);
        }

        [Fact]
        public void ExtractTeamStats_ShouldWork_MissingData()
        {
            var league = new MockData().LeagueStandings;

            var actual = league.ExtractTeamStats("2000-01");

            Assert.Equal(222, actual[1].TeamID);
            Assert.Equal("Utah Jazz", actual[1].TeamName);
            Assert.Equal("East", actual[1].Conference);
            Assert.Equal(0, actual[1].Standing);
            Assert.Equal(8, actual[1].Wins);
            Assert.Equal(2, actual[1].Losses);
            Assert.Equal(0.8, actual[1].Ratio);
            Assert.Equal(null, actual[1].Streak);
        }
        [Fact]
        public void ExtractTeamStats_ShouldWork_GetsAllEntries()
        {
            var league = new MockData().LeagueStandings;

            var actual = league.ExtractTeamStats("2000-01");

            Assert.True(actual.Count == 4);
        }

        #endregion

        [Fact]
        public void ExtractTeamData_ShouldWork_GetsAllEntries()
        {
            var franchises = new MockData().FranchiseData;

            var actual = franchises.ExtractTeamData();

            Assert.True(actual.Count == 3);
        }

        [Fact]
        public void ExtractTeamData_ShouldWork_IsDataIntact()
        {
            var franchises = new MockData().FranchiseData;

            var actual = franchises.ExtractTeamData();

            Assert.Equal(111,actual[0].TeamID);
            Assert.Equal("Chicago Bulls",actual[0].TeamName);
            Assert.Equal(1970,actual[0].Min_Year);
            Assert.Equal(2018,actual[0].Max_Year);
        }
    }
}
