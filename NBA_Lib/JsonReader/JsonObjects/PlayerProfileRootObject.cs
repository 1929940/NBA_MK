using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class PlayerProfileRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<PlayerProfile> ExtractPlayerStats()
        {
            var perSeason = ResultSets[0].RowSet.Select(p => new PlayerProfile()
            {
                TeamID = Convert.ToInt32(p[3]),
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
            // Collect Totals from the other list
            var inTotal = ResultSets[1].RowSet.Select(p => new PlayerProfile()
            {
                //TeamID = Convert.ToInt32(p[2]),
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
