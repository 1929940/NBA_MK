using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    class TeamSeasonsRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<TeamSeasons> ExtractSeasons()
        {
            var output = ResultSets[0].RowSet.Select(s => new TeamSeasons()
            {
                TeamID = Convert.ToInt32(s[1]),
                Min_Year = Convert.ToInt32(s[2]),
                Max_Year = Convert.ToInt32(s[3])
            });
            return output.ToList();
        }
    }
}
