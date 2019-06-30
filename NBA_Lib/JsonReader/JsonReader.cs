using NBA_Lib.JsonReader.JsonObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader
{
    public static class JsonReader
    {
        public static void GetTeams()
        {
            WebClient wc1 = new WebClient();

            var link = "http://stats.nba.com/stats/leaguestandingsv3?LeagueID=00&Season=2015-16&SeasonType=Regular+Season";
            wc1.Headers.Add("accept-encoding", "Accepflate, sdch");
            wc1.Headers.Add("Accept-Language", "en");
            wc1.Headers.Add("origin", "http://stats.nba.com");
            //wc1.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36");
            var json1 = wc1.DownloadString(link);

            var output = JsonConvert.DeserializeObject<TeamRootObject>(json1);
        }

        public static async Task<List<Team>> GetTeamsAsync()
        {
            using (var client = new HttpClient())
            {
                string link = "http://stats.nba.com/stats/leaguestandingsv3?LeagueID=00&Season=2015-16&SeasonType=Regular+Season";

                client.DefaultRequestHeaders.Add("accept-encoding", "Accepflate, sdch");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("origin", "http://stats.nba.com");

                string content = await client.GetStringAsync(link);

                TeamRootObject output = JsonConvert.DeserializeObject<TeamRootObject>(content);

                return output.ExtractTeams();
            }
        }
    }
}
