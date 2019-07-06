using System;
using System.Collections.Generic;
using System.Linq;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class TeamRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<Team> ExtractTeams(string season)
        {
            int  year = Convert.ToInt32(season.Substring(0, 4));
            IEnumerable<Team> output;

            if (year > 1969)
            {
                output = ResultSets[0].RowSet.Select(s => new Team()
                {
                    TeamID = Convert.ToInt32(s[2]),
                    TeamName = (s[3] + " " + s[4]),
                    Conference = (string)s[5],
                    Standing = Convert.ToInt32(s[7]),
                    Wins = Convert.ToInt32(s[12]),
                    Losses = Convert.ToInt32(s[13]),
                    Ratio = Convert.ToDouble(s[14]),
                    Streak = (s[35] == null) ? null : (int?)Convert.ToInt32(s[35])
                });
            }
            else
            {
                output = ResultSets[0].RowSet.Select(s => new Team()
                {
                    TeamID = Convert.ToInt32(s[2]),
                    TeamName = (s[3] + " " + s[4]),
                    Conference = (string)s[9],
                    Standing = Convert.ToInt32(s[7]),
                    Wins = Convert.ToInt32(s[12]),
                    Losses = Convert.ToInt32(s[13]),
                    Ratio = Convert.ToDouble(s[14]),
                    Streak = (s[35] == null) ? null : (int?)Convert.ToInt32(s[35])
                });
            }
            return output.ToList();
        }

    }
}
