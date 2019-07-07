using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NBA_Lib.JsonReader;

namespace NBA_Test
{
    public class JsonReader_Test
    {
        #region GetTeamsAsync
        [Theory]
        [InlineData("2018-19")]
        [InlineData("1946-47")]
        [InlineData("2000")]
        public async void GetTeamsAsync_ShouldWork_ValidData(string season)
        {
            var actual = await JsonReader.GetTeamsAsync(season);
            int numberOfRows = actual.ResultSets[0].RowSet.Count;

            Assert.True(numberOfRows > 0);
        }
        [Theory]
        [InlineData("2019-20")]
        [InlineData("1945-46")]
        public async void GetTeamsAsync_ShouldWork_InvalidData(string season)
        {
            var actual = await JsonReader.GetTeamsAsync(season);
            int numberOfRows = actual.ResultSets[0].RowSet.Count;

            Assert.True(numberOfRows == 0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetTeamsAsync_ShouldFail_InvalidParameter(string season)
        {
            await Assert.ThrowsAsync<System.Net.Http.HttpRequestException>(() => JsonReader.GetTeamsAsync(season));
        }
        #endregion

        #region GetFranchiseDataAsync

        [Fact]
        public async void GetFranchiseDataAsync_ShouldWork()
        {
            var actual = await JsonReader.GetFranchiseDataAsync();
            int numberOfRows = actual.ResultSets[0].RowSet.Count;

            Assert.True(numberOfRows > 0);
        }

        #endregion

        #region GetTeamRoosterAsync

        [Theory]
        [InlineData(1610612737, "2018-19")]
        [InlineData(1610610035, "1946-47")]
        [InlineData(1610612738, "2019-20")]
        public async void GetTeamRoosterAsync_ShouldWork_ValidData(int teamId, string season)
        {
            var actual = await JsonReader.GetTeamRosterAsync(teamId, season);
            int numberOfRows = actual.ResultSets[0].RowSet.Count;

            Assert.True(numberOfRows > 0);
        }
        [Theory]
        [InlineData(1610612738, "1945-46")]
        public async void GetTeamRoosterAsync_ShouldWork_InvalidData(int teamId, string season)
        {
            var actual = await JsonReader.GetTeamRosterAsync(teamId, season);
            int numberOfRows = actual.ResultSets[0].RowSet.Count;

            Assert.True(numberOfRows == 0);
        }

        #endregion

        #region GetPlayerProfile

        [Theory]
        [InlineData(2544)]
        [InlineData(2772)]
        [InlineData(76089)]
        public async void GetPlayerProfileAsync_ShouldWork_ValidData(int playerId)
        {
            var actual = await JsonReader.GetPlayerProfile(playerId);
            int numberOfRows = actual.ResultSets[0].RowSet.Count;

            Assert.True(numberOfRows > 0);
        }
        [Theory]
        [InlineData(25440)]
        public async void GetPlayerProfile_ShouldWork_InvalidData(int playerId)
        {
            var actual = await JsonReader.GetPlayerProfile(playerId);
            int numberOfRows = actual.ResultSets[0].RowSet.Count;

            Assert.True(numberOfRows == 0);
        }

        #endregion
    }
}
