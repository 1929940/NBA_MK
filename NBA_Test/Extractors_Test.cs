using System;
using System.Linq;
using Xunit;

namespace NBA_Test
{
    public class Extractors_Test
    {
        #region ExtractTeamStats

        [Fact]
        public void ExtractTeamStats_ShouldWork_ValidDataPre1970()
        {
            var actual= new MockData().LeagueStandings.ExtractTeamStats("1960-61");

            Assert.Equal("West", actual[0].Conference);
        }

        [Fact]
        public void ExtractTeamStats_ShouldWork_ValidDataPost1970()
        {
            var actual = new MockData().LeagueStandings.ExtractTeamStats("2000-01");

            Assert.Equal(111, actual[0].TeamID);
            Assert.Equal("Chicago Bulls", actual[0].TeamName);
            Assert.Equal("East", actual[0].Conference);
            Assert.Equal(2, actual[0].Standing);
            Assert.Equal(9, actual[0].Wins);
            Assert.Equal(1, actual[0].Losses);
            Assert.Equal(0.9, actual[0].Ratio);
            Assert.Equal(5, actual[0].Streak);
        }

        [Fact]
        public void ExtractTeamStats_ShouldWork_MissingData()
        {
            var actual = new MockData().LeagueStandings.ExtractTeamStats("2000-01");

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
            var actual = new MockData().LeagueStandings.ExtractTeamStats("2000-01");

            Assert.Equal(4, actual.Count);
        }
        #endregion

        #region ExtractTeamData
        [Fact]
        public void ExtractTeamData_ShouldWork_GetsAllEntries()
        {
            var actual = new MockData().FranchiseData.ExtractTeamData();

            Assert.Equal(4, actual.Count);
        }

        [Fact]
        public void ExtractTeamData_ShouldWork_IsDataIntact()
        {
            var actual = new MockData().FranchiseData.ExtractTeamData();

            Assert.Equal(111,actual[0].TeamID);
            Assert.Equal("Chicago Bulls",actual[0].TeamName);
            Assert.Equal(1970,actual[0].Min_Year);
            Assert.Equal(2018,actual[0].Max_Year);
        }
        #endregion

        #region ExtractPlayerStats

        [Fact]
        public void ExtractPlayerStats_ShouldWork_GetsAllEntries()
        {
            int numberOfEntries = new MockData().PlayerProfile.ExtractPlayerStats().Count;

            Assert.Equal(6, numberOfEntries);
        }

        [Theory]
        [InlineData(null, 111, "2000-01")]
        [InlineData(1, 444, "2001-02")]
        [InlineData(3, -1, "Total")]
        [InlineData(1, 0, "2001-02")]
        public void ExtractPlayerStats_ShouldWork_VerifyData( int? expected, int teamId, string season )
        {
            //Verifies if the mockData has not been adjusted in an uncontrolled manner
            //during extraction. 

            var data = new MockData().PlayerProfile.ExtractPlayerStats();

            var actual = (season == "Total") 
                ? data.FirstOrDefault(p => (p.TeamID == teamId))
                : data.FirstOrDefault(p => (p.TeamID == teamId) && (p.SeasonID == season));

            double? expectedDouble = (expected == null) ? null : (double?)1.0;

            Assert.Equal(teamId, actual.TeamID);
            Assert.True((season == actual.SeasonID) || (actual.SeasonID is null));

            Assert.Equal(expected, actual.GamesPlayed);
            Assert.Equal(expected, actual.GamesStarted);
            Assert.Equal(expected, actual.MinutesPlayed);

            Assert.Equal(expected, actual.FieldGoalsMade);
            Assert.Equal(expected, actual.FieldGoalsAttempted);
            Assert.Equal(expectedDouble, actual.FieldGoalsPercentage);

            Assert.Equal(expected, actual.ThreePointFieldGoalsMade);
            Assert.Equal(expected, actual.ThreePointFieldGoalsAttempted);
            Assert.Equal(expectedDouble, actual.ThreePointsFieldGoalPercentage);

            Assert.Equal(expected, actual.FreeThrowsMade);
            Assert.Equal(expected, actual.FreeThrowsAttempted);
            Assert.Equal(expectedDouble, actual.FreeThrowPercentage);

            Assert.Equal(expected, actual.OffensiveRebounds);
            Assert.Equal(expected, actual.DefensiveRebounds);
            Assert.Equal(expected, actual.Rebounds);

            Assert.Equal(expected, actual.Points);
            Assert.Equal(expected, actual.Assists);
            Assert.Equal(expected, actual.Steals);
            Assert.Equal(expected, actual.Blocks);
            Assert.Equal(expected, actual.Turnover);
            Assert.Equal(expected, actual.PersonalFouls);
        }

        #endregion
    }
}
