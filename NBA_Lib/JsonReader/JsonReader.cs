﻿using NBA_Lib.JsonReader.JsonObjects;
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
        public static async Task<List<Team>> GetTeamsAsync(string season)
        {
            using (var client = new HttpClient())
            {
                string link = String.Format("http://stats.nba.com/stats/leaguestandingsv3?LeagueID=00&Season={0}&SeasonType=Regular+Season" , season);

                client.DefaultRequestHeaders.Add("accept-encoding", "Accepflate, sdch");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("origin", "http://stats.nba.com");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");

                string content = await client.GetStringAsync(link);

                TeamRootObject output = JsonConvert.DeserializeObject<TeamRootObject>(content);

                return output.ExtractTeams();
            }
        }
        public static async Task<List<TeamSeasons>> GetTeamSeasonsAsync()
        {
            using (var client = new HttpClient())
            {
                string link = "https://stats.nba.com/stats/commonteamyears?LeagueID=00";

                client.DefaultRequestHeaders.Add("accept-encoding", "Accepflate, sdch");
                client.DefaultRequestHeaders.Add("Accept-Language", "en");
                client.DefaultRequestHeaders.Add("origin", "http://stats.nba.com");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");


                string content = await client.GetStringAsync(link);

                TeamSeasonsRootObject output = JsonConvert.DeserializeObject<TeamSeasonsRootObject>(content);

                return output.ExtractSeasons();
            }

        }

    }
}
