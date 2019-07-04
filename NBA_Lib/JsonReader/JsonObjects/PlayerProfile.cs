using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class PlayerProfile
    {
        public string SeasonID { get; set; }
        public int TeamID { get; set; }

        public int GamesPlayed { get; set; }
        public int GamesStarted { get; set; }
        public int MinutesPlayed { get; set; }

        public int FieldGoalsMade { get; set; }
        public int FieldGoalsAttempted { get; set; }
        public double FieldGoalsPercentage { get; set; }

        public int ThreePointFieldGoalsMade { get; set; }
        public int ThreePointFieldGoalsAttempted { get; set; }
        public double ThreePointsFieldGoalPercentage { get; set; }

        public int FreeThrowsMade { get; set; }
        public int FreeThrowsAttempted { get; set; }
        public double FreeThrowPercentage { get; set; }

        public int OffensiveRebounds { get; set; }
        public int DefensiveRebounds { get; set; }
        public int Rebounds { get; set; }

        public int Points { get; set; }
        public int Assists { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnover { get; set; }
        public int PersonalFouls { get; set; }

        public static List<string> GetSeasons(List<PlayerProfile> list, int id = 0)
        {
            HashSet<string> output;

            if (id != 0) output = list.Where(p => p.TeamID == id).Select(p => p.SeasonID).ToHashSet<string>();
            else output = list.Select(p => p.SeasonID).ToHashSet<string>();

            output.Remove(null);
            output.Add("All Seasons");

            return output.ToList();
        }
        public static List<int> GetTeamIDs(List<PlayerProfile> list)
        {
            List<int> output;

            output = list.Select(p => p.TeamID).Where(p => p != 0).Distinct().ToList();

            output.Add(0);

            return output;
        }
        private static string TranslateIdIntoName(int id, List<Franchise> franchises)
        {
            if (id == 0) return "All Teams";

            //PlayerProfileV2 Api sometimes provides an json without TeamID.
            //Im assigning to them the id of -1
            //And discarding those entries

            //I think its when a player has played in several teams in one season
            //The teamid-less records are a sum for that season
            if (id == -1) return string.Empty;

            var tmp = franchises.FirstOrDefault(t => t.TeamID == id).TeamName;

            return tmp;
        }
        public static Dictionary<int, string> GetIdNameDictionary(List<PlayerProfile> playerProfiles, List<Franchise> franchises)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();

            foreach (var player in playerProfiles)
            {
                int id = player.TeamID;
                string name = TranslateIdIntoName(id, franchises);

                if  (!output.Keys.Contains(id) && id != -1)
                    output.Add(id, name);
            }
            return output;
        }
        public static PlayerProfile GetTotalStatsInTeam(int id, List<PlayerProfile> playerProfiles)
        {
            PlayerProfile output = new PlayerProfile();

            foreach (var profile in playerProfiles)
            {
                if (profile.TeamID == id)
                {
                    output.GamesPlayed += profile.GamesPlayed;
                    output.GamesStarted += profile.GamesStarted;
                    output.MinutesPlayed += profile.MinutesPlayed;

                    output.FieldGoalsMade += profile.FieldGoalsMade;
                    output.FieldGoalsAttempted += profile.FieldGoalsAttempted;

                    output.ThreePointFieldGoalsMade += profile.ThreePointFieldGoalsMade;
                    output.ThreePointFieldGoalsAttempted += profile.ThreePointFieldGoalsAttempted;

                    output.FreeThrowsMade += profile.FreeThrowsMade;
                    output.FreeThrowsAttempted += profile.FreeThrowsAttempted;

                    output.OffensiveRebounds += profile.OffensiveRebounds;
                    output.DefensiveRebounds += profile.DefensiveRebounds;
                    output.Rebounds += profile.Rebounds;

                    output.Points += profile.Points;
                    output.Assists += profile.Assists;
                    output.Steals += profile.Steals;
                    output.Blocks += profile.Blocks;
                    output.Turnover += profile.Turnover;
                    output.PersonalFouls += profile.PersonalFouls;
                }
            }
            output.FieldGoalsPercentage = (output.FieldGoalsAttempted > 0) ?
                Math.Round((double)output.FieldGoalsMade / (double)output.FieldGoalsAttempted, 3) : 0;

            output.ThreePointsFieldGoalPercentage = (output.ThreePointFieldGoalsAttempted > 0) ? 
                Math.Round((double)output.ThreePointFieldGoalsMade / (double)output.ThreePointFieldGoalsAttempted, 3) : 0;

            output.FreeThrowPercentage = (output.FreeThrowPercentage > 0) ?
                Math.Round((double)output.FreeThrowsMade / (double)output.FreeThrowsAttempted, 3) : 0;

            return output;
        }
        public override string ToString()
        {
            string output = String.Format($"SeasonID: {SeasonID}\n" +
                $"TeamID: {TeamID}\n" +
                $"Games Played: {GamesPlayed}\n" +
                $"Games Started: {GamesStarted}\n" +
                $"Minutes Played: {MinutesPlayed}\n" +
                $"Field Goals Made: {FieldGoalsMade}\n" +
                $"Field Goals Attempted: {FieldGoalsAttempted}\n" +
                $"Field Goal Percentage: {FieldGoalsPercentage}\n" +
                $"Three Points Field Goals Made: {ThreePointFieldGoalsMade}\n" +
                $"Three Points Field Goals Attempted: {ThreePointFieldGoalsAttempted}\n" +
                $"Three Points Field Goal Percentange: {ThreePointsFieldGoalPercentage}\n" +
                $"Free Throws Made: {FreeThrowsMade}\n" +
                $"Free Throws Attempted: {FreeThrowsAttempted}\n" +
                $"Free Throws Percentage: {FreeThrowPercentage}\n" +
                $"Offensive Rebounds: {OffensiveRebounds}\n" +
                $"Defensive Rebounds: {DefensiveRebounds}\n" +
                $"Rebounds: {Rebounds}\n" +
                $"Points: {Points}\n" +
                $"Assists: {Assists}\n" +
                $"Steals: {Steals}\n" +
                $"Blocks: {Blocks}\n" +
                $"Turnovers: {Turnover}\n" +
                $"Personal Fouls: {PersonalFouls}\n\n\n");

            return output;
        }
    }
}
