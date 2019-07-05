using System;
using System.Collections.Generic;
using System.Linq;

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

        public static List<string> GetSeasons(List<PlayerProfile> list, int id = -1)
        {
            // A player can play in more than one team per season
            // HashSet ensures there are no duplicate seasons

            HashSet<string> output;

            if (id != -1) output = list.Where(p => p.TeamID == id).Select(p => p.SeasonID).ToHashSet<string>();
            else output = list.Select(p => p.SeasonID).ToHashSet<string>();

            output.Remove(null);
            output.Add("All Seasons");

            return output.ToList();
        }
        public static List<int> GetTeamIDs(List<PlayerProfile> list)
        {
            List<int> output;

            output = list.Select(p => p.TeamID).Where(p => p != -1).Distinct().ToList();

            output.Add(-1);

            Console.WriteLine("Yes: GetTeamIDs is active");

            return output;
        }
        private static string TranslateIdIntoName(int id, List<Franchise> franchises)
        {
            if (id == 0) return string.Empty;

            if (id == -1) return "All Teams";

            var tmp = franchises.FirstOrDefault(t => t.TeamID == id).TeamName;

            return tmp;
        }
        public static Dictionary<int, string> GetTeamIdNameDictionary(List<PlayerProfile> playerProfiles, List<Franchise> franchises)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();

            foreach (var player in playerProfiles)
            {
                int id = player.TeamID;
                string name = TranslateIdIntoName(id, franchises);

                if  (!output.Keys.Contains(id) && id != 0)
                    output.Add(id, name);
            }
            return output;
        }
        public static PlayerProfile GetPlayersTotalStatsWhileInTeam(int teamId, List<PlayerProfile> playerProfiles)
        {
            PlayerProfile output = new PlayerProfile();

            foreach (var profile in playerProfiles)
            {
                if (profile.TeamID == teamId)
                {
                    output.GamesPlayed += 
                        (profile.GamesPlayed == -100) ? 0 : profile.GamesPlayed;
                    output.GamesStarted += 
                        (profile.GamesStarted == -100) ? 0 : profile.GamesStarted;
                    output.MinutesPlayed += 
                        (profile.MinutesPlayed == -100) ? 0 : profile.MinutesPlayed;

                    output.FieldGoalsMade += 
                        (profile.FieldGoalsMade == -100) ? 0 : profile.FieldGoalsMade;
                    output.FieldGoalsAttempted += 
                        (profile.FieldGoalsAttempted == -100) ? 0 : profile.FieldGoalsAttempted;

                    output.ThreePointFieldGoalsMade += 
                        (profile.ThreePointFieldGoalsMade == -100) ? 0 : profile.ThreePointFieldGoalsMade;
                    output.ThreePointFieldGoalsAttempted += 
                        (profile.ThreePointFieldGoalsAttempted == -100) ? 0 : profile.ThreePointFieldGoalsAttempted;

                    output.FreeThrowsMade += 
                        (profile.FreeThrowsMade == -100) ? 0 : profile.FreeThrowsMade;
                    output.FreeThrowsAttempted += 
                        (profile.FreeThrowsAttempted == -100) ? 0 : profile.FreeThrowsAttempted;

                    output.OffensiveRebounds += 
                        (profile.OffensiveRebounds == -100) ? 0 : profile.OffensiveRebounds;
                    output.DefensiveRebounds += 
                        (profile.DefensiveRebounds == -100) ? 0 : profile.DefensiveRebounds;
                    output.Rebounds += 
                        (profile.Rebounds == -100) ? 0 : profile.Rebounds;

                    output.Points += (profile.Points == -100) ? 0 : profile.Points;
                    output.Assists += (profile.Assists == -100) ? 0 : profile.Assists;
                    output.Blocks += (profile.Blocks == -100) ? 0 : profile.Blocks;
                    output.Steals += (profile.Steals == -100) ? 0 : profile.Steals;
                    output.Turnover += (profile.Turnover == -100) ? 0 : profile.Turnover;
                    output.PersonalFouls += (profile.PersonalFouls == -100) ? 0 : profile.PersonalFouls;
                }
            }
            output.FieldGoalsPercentage = (output.FieldGoalsAttempted > 0) ?
                Math.Round((double)output.FieldGoalsMade / (double)output.FieldGoalsAttempted, 3) : 0;

            output.ThreePointsFieldGoalPercentage = (output.ThreePointFieldGoalsAttempted > 0) ? 
                Math.Round((double)output.ThreePointFieldGoalsMade / (double)output.ThreePointFieldGoalsAttempted, 3) : 0;

            output.FreeThrowPercentage = (output.FreeThrowsAttempted > 0) ?
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
