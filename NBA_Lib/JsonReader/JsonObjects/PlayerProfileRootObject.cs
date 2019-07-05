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
                //PlayerProfileV2 API returns multiple entries for that season.
                //One for team played, and additional one for total stats of that season.
                //The additional entry has teamId = 0.

                //This code can be improved, however casting obj to (int?) and (obj as int?) seem to fail
                //Perhaps if I try adjusting resultset to give me a list of string instead of obj, this will help?
                //Parse wont work, TryParse might be an chaotic way to do it but perhaps?

                TeamID = Convert.ToInt32(p[3]),
                SeasonID = p[1].ToString(),

                GamesPlayed = (p[6] == null) ? -100 : Convert.ToInt32(p[6]),
                GamesStarted = (p[7] == null) ? -100 : Convert.ToInt32(p[7]),
                MinutesPlayed = (p[8] == null) ? -100 : Convert.ToInt32(p[8]),

                FieldGoalsMade = (p[9] == null) ? -100 : Convert.ToInt32(p[9]),
                FieldGoalsAttempted = (p[10] == null) ? -100 : Convert.ToInt32(p[10]),
                FieldGoalsPercentage = (p[11] == null) ? -100 : Convert.ToDouble(p[11]),

                ThreePointFieldGoalsMade = (p[12] == null) ? -100 : Convert.ToInt32(p[12]),
                ThreePointFieldGoalsAttempted = (p[13] == null) ? -100 : Convert.ToInt32(p[13]),
                ThreePointsFieldGoalPercentage = (p[14] == null) ? -100 : Convert.ToDouble(p[14]),

                FreeThrowsMade = (p[15] == null) ? -100 : Convert.ToInt32(p[15]),
                FreeThrowsAttempted = (p[16] == null) ? -100 : Convert.ToInt32(p[16]),
                FreeThrowPercentage = (p[17] == null) ? -100 : Convert.ToDouble(p[17]),

                OffensiveRebounds = (p[18] == null) ? -100 : Convert.ToInt32(p[18]),
                DefensiveRebounds = (p[19] == null) ? -100 : Convert.ToInt32(p[19]),
                Rebounds = (p[20] == null) ? -100 : Convert.ToInt32(p[20]),

                Points = (p[26] == null) ? -100 : Convert.ToInt32(p[26]),
                Assists = (p[21] == null) ? -100 : Convert.ToInt32(p[21]),
                Steals = (p[22] == null) ? -100 : Convert.ToInt32(p[22]),
                Blocks = (p[23] == null) ? -100 : Convert.ToInt32(p[23]),
                Turnover = (p[24] == null) ? -100 : Convert.ToInt32(p[24]),
                PersonalFouls = (p[25] == null) ? -100 : Convert.ToInt32(p[25])

            });
            // PlayerProfile with total statistics for all seasons.
            PlayerProfile inTotal = ResultSets[1].RowSet.Select(p => new PlayerProfile()
            {
                TeamID = -1,
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
            }).First();
            var output = perSeason.ToList();
            output.Add(inTotal);

            return output;
        }
    }
}
