using System;
using System.Collections.Generic;
using System.Linq;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class PlayerProfileRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<PlayerProfile> ExtractPlayerStats()
        {
            var perSeason = ResultSets[0].RowSet.Select(p => new PlayerProfile()
            {
                //When a player played in more than one team in a season
                //PlayerProfileV2 API returns json with TeamID equal 0 
                //with players total stats for the season.
                //Changing that to -1 since 0 is reserved for [ all teams ]
                //  0 = all teams
                // -1 = total stats for a season  

                TeamID = (p[3].ToString() == "0") ? -1 : Convert.ToInt32(p[3]),
                SeasonID = p[1].ToString(),

                GamesPlayed = Convert.ToInt32(p[6]),
                GamesStarted = Convert.ToInt32(p[7]),
                MinutesPlayed = Convert.ToInt32(p[8]),

                FieldGoalsMade = Convert.ToInt32(p[9]),
                FieldGoalsAttempted = Convert.ToInt32(p[10]),
                FieldGoalsPercentage = Convert.ToDouble(p[11]),

                ThreePointFieldGoalsMade = Convert.ToInt32(p[12]),
                ThreePointFieldGoalsAttempted = Convert.ToInt32(p[13]),
                ThreePointsFieldGoalPercentage = Convert.ToDouble(p[14]),

                FreeThrowsMade = Convert.ToInt32(p[15]),
                FreeThrowsAttempted = Convert.ToInt32(p[16]),
                FreeThrowPercentage = Convert.ToDouble(p[17]),

                OffensiveRebounds = Convert.ToInt32(p[18]),
                DefensiveRebounds = Convert.ToInt32(p[19]),
                Rebounds = Convert.ToInt32(p[20]),

                Points = Convert.ToInt32(p[26]),
                Assists = Convert.ToInt32(p[21]),
                Steals = Convert.ToInt32(p[22]),
                Blocks = Convert.ToInt32(p[23]),
                Turnover = Convert.ToInt32(p[24]),
                PersonalFouls = Convert.ToInt32(p[25])

            });
            // PlayerProfile with total statistics for all seasons.
            var inTotal = ResultSets[1].RowSet.Select(p => new PlayerProfile()
            {
                //TeamID = Convert.ToInt32(p[2]), should make it -1
                //SeasonID = p[1].ToString(), This is null

                GamesPlayed = Convert.ToInt32(p[3]),
                GamesStarted = Convert.ToInt32(p[4]),
                MinutesPlayed = Convert.ToInt32(p[5]),

                FieldGoalsMade = Convert.ToInt32(p[6]),
                FieldGoalsAttempted = Convert.ToInt32(p[7]),
                FieldGoalsPercentage = Convert.ToDouble(p[8]),

                ThreePointFieldGoalsMade = Convert.ToInt32(p[9]),
                ThreePointFieldGoalsAttempted = Convert.ToInt32(p[10]),
                ThreePointsFieldGoalPercentage = Convert.ToDouble(p[11]),

                FreeThrowsMade = Convert.ToInt32(p[12]),
                FreeThrowsAttempted = Convert.ToInt32(p[13]),
                FreeThrowPercentage = Convert.ToDouble(p[14]),

                OffensiveRebounds = Convert.ToInt32(p[15]),
                DefensiveRebounds = Convert.ToInt32(p[16]),
                Rebounds = Convert.ToInt32(p[17]),

                Points = Convert.ToInt32(p[23]),
                Assists = Convert.ToInt32(p[18]),
                Steals = Convert.ToInt32(p[19]),
                Blocks = Convert.ToInt32(p[20]),
                Turnover = Convert.ToInt32(p[21]),
                PersonalFouls = Convert.ToInt32(p[22])
            });
            var output = perSeason.ToList();
            output.AddRange(inTotal);

            return output;
        }
    }
}
