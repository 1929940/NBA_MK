using NBA_Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NBA_Lib.JsonReader.JsonRoots
{
    public class TeamRoosterRootObject
    {
        public List<ResultSet> ResultSets { get; set; }

        public List<TeamMembers> ExtractTeamMembers()
        {
            var players = ResultSets[0].RowSet.Select(r => new TeamMembers()
            {
                PlayerName = r[3].ToString(),
                Number = r[4]?.ToString(),
                Position = r[5]?.ToString(),
                Height = r[6]?.ToString(),
                Weight = Convert.ToInt32(r[7]),
                BirthDate = DateTime.Parse(r[8].ToString()),
                Age = Convert.ToInt32(r[9]),
                PlayerID = Convert.ToInt32(r[12]),
                Role = "Player"
            });

            var coaches = ResultSets[1].RowSet.Select(c => new TeamMembers()
            {
                PlayerName = c[5].ToString(),
                Position = c[8].ToString(),
                Role = "Coach"
            });

            var output = players.ToList();
            output.AddRange(coaches);

            return output;
        }
    }
}
