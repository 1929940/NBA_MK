using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.Model
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

        public static IEnumerable<Team> CorrectStanding(IEnumerable<Team> team)
        {
            /** LeagueStanding API in older seasons provides no information for teams standing.
            This sets their standing based on the teams win/loss ratio in comparison to other teams **/

            if (team.All(t => t.Standing == 0))
            {
                int index = 0;

                return team.OrderByDescending(t => t.Ratio).Select((t, standing) => { t.Standing = ++index; return t; });
            }
            return team;
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
