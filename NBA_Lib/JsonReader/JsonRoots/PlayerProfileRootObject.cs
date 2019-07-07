using NBA_Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NBA_Lib.JsonReader.JsonRoots
{
    public class PlayerProfileRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<PlayerStats> ExtractPlayerStats()
        {
            var perSeason = ResultSets[0].RowSet.Select(p => new PlayerStats()
            {
                //When a player played in more than one team in a season
                //PlayerProfileV2 API returns multiple entries for that season.
                //One for team played, and additional one for total stats of that season.
                //The additional entry has teamId = 0.

                TeamID = Convert.ToInt32(p[3]),
                SeasonID = p[1].ToString(),

                GamesPlayed   = (p[6] == null) ? null : (int?)Convert.ToInt32(p[6]),
                GamesStarted  = (p[7] == null) ? null : (int?)Convert.ToInt32(p[7]),
                MinutesPlayed = (p[8] == null) ? null : (int?)Convert.ToInt32(p[8]),

                FieldGoalsMade       = (p[9] == null) ? null : (int?)Convert.ToInt32(p[9]),
                FieldGoalsAttempted  = (p[10] == null) ? null : (int?)Convert.ToInt32(p[10]),
                FieldGoalsPercentage = (p[11] == null) ? null : (double?)Convert.ToDouble(p[11]),

                ThreePointFieldGoalsMade       = (p[12] == null) ? null : (int?)Convert.ToInt32(p[12]),
                ThreePointFieldGoalsAttempted  = (p[13] == null) ? null : (int?)Convert.ToInt32(p[13]),
                ThreePointsFieldGoalPercentage = (p[14] == null) ? null : (double?)Convert.ToDouble(p[14]),

                FreeThrowsMade      = (p[15] == null) ? null : (int?)Convert.ToInt32(p[15]),
                FreeThrowsAttempted = (p[16] == null) ? null : (int?)Convert.ToInt32(p[16]),
                FreeThrowPercentage = (p[17] == null) ? null : (double?)Convert.ToDouble(p[17]),

                OffensiveRebounds = (p[18] == null) ? null : (int?)Convert.ToInt32(p[18]),
                DefensiveRebounds = (p[19] == null) ? null : (int?)Convert.ToInt32(p[19]),
                Rebounds          = (p[20] == null) ? null : (int?)Convert.ToInt32(p[20]),

                Points   = (p[26] == null) ? null : (int?)Convert.ToInt32(p[26]),
                Assists  = (p[21] == null) ? null : (int?)Convert.ToInt32(p[21]),
                Steals   = (p[22] == null) ? null : (int?)Convert.ToInt32(p[22]),
                Blocks   = (p[23] == null) ? null : (int?)Convert.ToInt32(p[23]),
                Turnover = (p[24] == null) ? null : (int?)Convert.ToInt32(p[24]),
                PersonalFouls = (p[25] == null) ? null : (int?)Convert.ToInt32(p[25])

            });
            // PlayerProfile with total statistics for all seasons.
            PlayerStats inTotal = ResultSets[1].RowSet.Select(p => new PlayerStats()
            {
                TeamID = -1,

                GamesPlayed   = (p[3] == null) ? null : (int?)Convert.ToInt32(p[3]),
                GamesStarted  = (p[4] == null) ? null : (int?)Convert.ToInt32(p[4]),
                MinutesPlayed = (p[5] == null) ? null : (int?)Convert.ToInt32(p[5]),

                FieldGoalsMade       = (p[6] == null) ? null : (int?)Convert.ToInt32(p[6]),
                FieldGoalsAttempted  = (p[7] == null) ? null : (int?)Convert.ToInt32(p[7]),
                FieldGoalsPercentage = (p[8] == null) ? null : (double?)Convert.ToDouble(p[8]),

                ThreePointFieldGoalsMade       = (p[9] == null) ? null : (int?)Convert.ToInt32(p[9]),
                ThreePointFieldGoalsAttempted  = (p[10] == null) ? null : (int?)Convert.ToInt32(p[10]),
                ThreePointsFieldGoalPercentage = (p[11] == null) ? null : (double?)Convert.ToDouble(p[11]),

                FreeThrowsMade      = (p[12] == null) ? null : (int?)Convert.ToInt32(p[12]),
                FreeThrowsAttempted = (p[13] == null) ? null : (int?)Convert.ToInt32(p[13]),
                FreeThrowPercentage = (p[14] == null) ? null : (double?)Convert.ToDouble(p[14]),

                OffensiveRebounds = (p[15] == null) ? null : (int?)Convert.ToInt32(p[15]),
                DefensiveRebounds = (p[16] == null) ? null : (int?)Convert.ToInt32(p[16]),
                Rebounds          = (p[17] == null) ? null : (int?)Convert.ToInt32(p[17]),

                Points   = (p[23] == null) ? null : (int?)Convert.ToInt32(p[23]),
                Assists  = (p[18] == null) ? null : (int?)Convert.ToInt32(p[18]),
                Steals   = (p[19] == null) ? null : (int?)Convert.ToInt32(p[19]),
                Blocks   = (p[20] == null) ? null : (int?)Convert.ToInt32(p[20]),
                Turnover = (p[21] == null) ? null : (int?)Convert.ToInt32(p[21]),
                PersonalFouls = (p[22] == null) ? null : (int?)Convert.ToInt32(p[22])
            }).First();
            var output = perSeason.ToList();
            output.Add(inTotal);

            return output;
        }
    }
}
