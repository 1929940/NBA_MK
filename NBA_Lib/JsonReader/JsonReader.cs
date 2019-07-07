using NBA_Lib.JsonReader.JsonRoots;
using NBA_Lib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader
{
    public static class JsonReader
    {
        //LeagueStandings
        public static async Task<List<TeamStats>> GetTeamsAsync(string season)
        {
            using (var client = new HttpClient())
            {
                string link = String.Format("http://stats.nba.com/stats/leaguestandingsv3?LeagueID=00&Season={0}&SeasonType=Regular+Season" , season);

                client.DefaultRequestHeaders.Add("accept-encoding", "Accepflate, sdch");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("origin", "http://stats.nba.com");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");

                string content = await client.GetStringAsync(link);

                LeagueStandingsRootObject output = JsonConvert.DeserializeObject<LeagueStandingsRootObject>(content);

                return output.ExtractTeamStats(season);
            }
        }

        //Franchise History
        public static async Task<List<TeamData>> GetFranchiseDataAsync()
        {
            using (var client = new HttpClient())
            {
                string link = "https://stats.nba.com/stats/franchisehistory?LeagueID=00";

                client.DefaultRequestHeaders.Add("accept-encoding", "Accepflate, sdch");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("origin", "http://stats.nba.com");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");

                string content = await client.GetStringAsync(link);

                FranchiseHistoryRootObject output = JsonConvert.DeserializeObject<FranchiseHistoryRootObject>(content);

                return output.ExtractTeamData();
            }
        }

        //CommonTeamRooster
        public static async Task<List<TeamMembers>> GetTeamRosterAsync(int teamID, string season)
        {
            using (var client = new HttpClient())
            {
                string link = String.Format("https://stats.nba.com/stats/commonteamroster?LeagueID=00&Season={0}&TeamID={1}", season, teamID);

                client.DefaultRequestHeaders.Add("accept-encoding", "Accepflate, sdch");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("origin", "http://stats.nba.com");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");

                string content = await client.GetStringAsync(link);

                TeamRoosterRootObject output = JsonConvert.DeserializeObject<TeamRoosterRootObject>(content);

                return output.ExtractTeamMembers();
            }
        }
        //PlayerProfileV2
        public static async Task<List<PlayerStats>> GetPlayerProfile(int playerID)
        {
            using (var client = new HttpClient())
            {
                string link = String.Format("https://stats.nba.com/stats/playerprofilev2?LeagueID=00&PerMode=Totals&PlayerID={0}", playerID);

                client.DefaultRequestHeaders.Add("accept-encoding", "Accepflate, sdch");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("origin", "http://stats.nba.com");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");

                string content = await client.GetStringAsync(link);

                PlayerProfileRootObject output = JsonConvert.DeserializeObject<PlayerProfileRootObject>(content);

                return output.ExtractPlayerStats();
            }
        }
    }
}
