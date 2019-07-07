using NBA_Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NBA_Lib.JsonReader.JsonRoots
{
    public class FranchiseHistoryRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<TeamData> ExtractTeamData()
        {
            var activeFranchises = ResultSets[0].RowSet.Select(f => new TeamData()
            {
                TeamID = Convert.ToInt32(f[1]),
                TeamName = (f[2] + " " + f[3]),
                Min_Year = Convert.ToInt32(f[4]),
                Max_Year = Convert.ToInt32(f[5])
            });

            var defunctFranchises = ResultSets[1].RowSet.Select(f => new TeamData()
            {
                TeamID = Convert.ToInt32(f[1]),
                TeamName = (f[2] + " " + f[3]),
                Min_Year = Convert.ToInt32(f[4]),
                Max_Year = Convert.ToInt32(f[5])
            });
            var output = new List<TeamData>();

            output.AddRange(activeFranchises);
            output.AddRange(defunctFranchises);

            return output;
        }
    }
}
