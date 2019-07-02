using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class TeamRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<Team> ExtractTeams()
        {
            var output = ResultSets[0].RowSet.Select(s => new Team()
            {
                TeamID = Convert.ToInt32(s[2]),
                TeamName = (s[3] + " " + s[4]),
                Conference = (string)s[5],
                Standing = Convert.ToInt32(s[7]),
                Wins = Convert.ToInt32(s[12]),
                Losses = Convert.ToInt32(s[13]),
                Ratio = Convert.ToDouble(s[14]),
                Streak = s[35].ToString()
            });

            return output.ToList();
        }
    }
}
