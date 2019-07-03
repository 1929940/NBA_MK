using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string Conference { get; set; }
        public int Standing { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double Ratio { get; set; }
        public int? Streak { get; set; }

        public static string TranslateIdIntoName(int id, List<Team> teams)
        {
            string output = teams.FirstOrDefault(t => t.TeamID == id).TeamName;

            return output;
        }

        public override string ToString()
        {
            string output = String.Format("TeamId: {0} \n" +
                "TeamName: {1} \n" +
                "Conference: {2} \n" +
                "Standing: {3} \n" +
                "Wins: {4} \n" +
                "Losses: {5} \n" +
                "Ratio: {6} \n" +
                "Streak: {7} \n", 
                TeamID, TeamName, Conference, Standing, 
                Wins, Losses, Ratio, Streak);

            return output;
        }
    }
}
