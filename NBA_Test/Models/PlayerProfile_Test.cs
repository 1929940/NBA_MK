using Xunit;
using NBA_Lib.Model;

namespace NBA_Test.Models
{
    public class PlayerProfile_Test
    {
        [Fact]
        public void GetTeamIdNameDictionary_ShouldWork_TranslateMockData()
        {
            var mockData = new MockData();

            var teams = mockData.FranchiseData.ExtractTeamData();
            var playerStats = mockData.PlayerProfile.ExtractPlayerStats();

            var actual = PlayerStats.GetTeamIdNameDictionary(playerStats, teams);

            Assert.Equal("All Teams", actual[-1]);
            Assert.Equal("Chicago Bulls", actual[111]);
            Assert.Equal("LA Clippers", actual[444]);


        }

        [Theory]
        [InlineData()]
        [InlineData(111)]
        [InlineData(222)]
        public void GetTeamSeasons_ShouldWork_NoInvalidData(int teamId = -1)
        {
            var playerStats = new MockData().PlayerProfile.ExtractPlayerStats();

            var actual = PlayerStats.GetSeasons(playerStats, teamId);

            Assert.DoesNotContain(actual, p => p == "");
            Assert.DoesNotContain(actual, p => p is null);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(2, 0)]
        [InlineData(3,111)]
        [InlineData(1,222)]
        public void GetTeamSeasons_ShouldWork_DataCountCheck(int expected , int teamId = -1)
        {
            var playerStats = new MockData().PlayerProfile.ExtractPlayerStats();

            var actual = PlayerStats.GetSeasons(playerStats, teamId);

            Assert.Equal(expected, actual.Count);
        }

        [Theory]
        [InlineData(111, null)]
        [InlineData(444, 3)]
        public void GetPlayersTotalStatsWhileInTeam_ShouldWork_VerifyReturnData(int teamId, int? expected)
        {
            var playerStats = new MockData().PlayerProfile.ExtractPlayerStats();

            var actual = PlayerStats.GetPlayersTotalStatsWhileInTeam(teamId, playerStats);

            double? expectedDouble = (expected == null) ? null : (double?)1;

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

    }
}
