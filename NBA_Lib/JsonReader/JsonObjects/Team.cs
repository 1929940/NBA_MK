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

        //public static string TranslateIdIntoName(int id, List<Franchise> franchises)
        //{
        //    if (id == 0) return "All Teams";

        //    return franchises.FirstOrDefault(t => t.TeamID == id).TeamName;
        //}
        //public static Dictionary<int, string> GetIdNameDictionary(List<PlayerProfile> playerProfiles, List<Franchise> franchises)
        //{
        //    Dictionary<int, string> output = new Dictionary<int, string>();

        //    //output.Add

        //    //var teams = PlayerProfile.GetTeamIDs(playerProfiles);

        //    //Teams_CBX.ItemsSource = teams.Select(f => Team.TranslateIdIntoName(f, franchises));

        //}


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
