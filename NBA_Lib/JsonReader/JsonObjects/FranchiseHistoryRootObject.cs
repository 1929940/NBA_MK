using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class FranchiseHistoryRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<Franchise> ExtractFranchises()
        {
            var output = ResultSets[0].RowSet.Select(f => new Franchise()
            {
                TeamID = Convert.ToInt32(f[1]),
                TeamName = (f[2] + " " + f[3]),
                Min_Year = Convert.ToInt32(f[4]),
                Max_Year = Convert.ToInt32(f[5])
            });

            return output.ToList();
        }
    }
}
