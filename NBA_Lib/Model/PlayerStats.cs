using System;
using System.Collections.Generic;
using System.Linq;

namespace NBA_Lib.Model
{
    public class PlayerStats
    {
        public string SeasonID { get; set; }
        public int TeamID { get; set; }

        public int? GamesPlayed { get; set; }
        public int? GamesStarted { get; set; }
        public int? MinutesPlayed { get; set; }

        public int? FieldGoalsMade { get; set; }
        public int? FieldGoalsAttempted { get; set; }
        public double? FieldGoalsPercentage { get; set; }

        public int? ThreePointFieldGoalsMade { get; set; }
        public int? ThreePointFieldGoalsAttempted { get; set; }
        public double? ThreePointsFieldGoalPercentage { get; set; }

        public int? FreeThrowsMade { get; set; }
        public int? FreeThrowsAttempted { get; set; }
        public double? FreeThrowPercentage { get; set; }

        public int? OffensiveRebounds { get; set; }
        public int? DefensiveRebounds { get; set; }
        public int? Rebounds { get; set; }

        public int? Points { get; set; }
        public int? Assists { get; set; }
        public int? Steals { get; set; }
        public int? Blocks { get; set; }
        public int? Turnover { get; set; }
        public int? PersonalFouls { get; set; }

        public static List<string> GetSeasons(List<PlayerStats> playerStats, int id = -1)
        {
            // A player can play in more than one team per season
            // HashSet ensures there are no duplicate seasons

            HashSet<string> output;

            if (id != -1) output = playerStats.Where(p => p.TeamID == id).Select(p => p.SeasonID).ToHashSet<string>();
            else output = playerStats.Select(p => p.SeasonID).ToHashSet<string>();

            output.Remove(null);
            output.Add("All Seasons");

            return output.ToList();
        }
        private static string TranslateIdIntoName(int teamId, List<TeamData> teams)
        {
            if (teamId == 0) return string.Empty;

            if (teamId == -1) return "All Teams";

            var tmp = teams.FirstOrDefault(t => t.TeamID == teamId).TeamName;

            return tmp;
        }
        public static Dictionary<int, string> GetTeamIdNameDictionary(List<PlayerStats> playerStats, List<TeamData> teams)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();

            foreach (var player in playerStats)
            {
                int teamId = player.TeamID;
                string name = TranslateIdIntoName(teamId, teams);

                if  (!output.Keys.Contains(teamId) && teamId != 0)
                    output.Add(teamId, name);
            }
            return output;
        }
        public static PlayerStats GetPlayersTotalStatsWhileInTeam(int teamId, List<PlayerStats> playerStats)
        {
            PlayerStats output = new PlayerStats();

            foreach (var profile in playerStats)
            {
                if (profile.TeamID == teamId)
                {
                    output.GamesPlayed = PlayerStats.AddValueToTotal(output.GamesPlayed, profile.GamesPlayed);
                    output.GamesStarted = PlayerStats.AddValueToTotal(output.GamesStarted, profile.GamesStarted);
                    output.MinutesPlayed = PlayerStats.AddValueToTotal(output.MinutesPlayed, profile.MinutesPlayed);

                    output.FieldGoalsMade = PlayerStats.AddValueToTotal(output.FieldGoalsMade, profile.FieldGoalsMade);
                    output.FieldGoalsAttempted = PlayerStats.AddValueToTotal(output.FieldGoalsAttempted, profile.FieldGoalsAttempted);

                    output.ThreePointFieldGoalsMade = PlayerStats.AddValueToTotal(output.ThreePointFieldGoalsMade, profile.ThreePointFieldGoalsMade);
                    output.ThreePointFieldGoalsAttempted = PlayerStats.AddValueToTotal(output.ThreePointFieldGoalsAttempted, profile.ThreePointFieldGoalsAttempted);

                    output.FreeThrowsMade = PlayerStats.AddValueToTotal(output.FreeThrowsMade, profile.FreeThrowsMade);
                    output.FreeThrowsAttempted = PlayerStats.AddValueToTotal(output.FreeThrowsAttempted, profile.FreeThrowsAttempted);

                    output.OffensiveRebounds = PlayerStats.AddValueToTotal(output.OffensiveRebounds, profile.OffensiveRebounds);
                    output.DefensiveRebounds = PlayerStats.AddValueToTotal(output.DefensiveRebounds, profile.DefensiveRebounds);
                    output.Rebounds = PlayerStats.AddValueToTotal(output.Rebounds, profile.Rebounds);

                    output.Points = PlayerStats.AddValueToTotal(output.Points, profile.Points);
                    output.Assists = PlayerStats.AddValueToTotal(output.Assists, profile.Assists);
                    output.Blocks = PlayerStats.AddValueToTotal(output.Blocks, profile.Blocks);
                    output.Steals = PlayerStats.AddValueToTotal(output.Steals, profile.Steals);
                    output.Turnover = PlayerStats.AddValueToTotal(output.Turnover, profile.Turnover);
                    output.PersonalFouls = PlayerStats.AddValueToTotal(output.PersonalFouls, profile.PersonalFouls);
                }
            }
            output.FieldGoalsPercentage = (output.FieldGoalsAttempted > 0) ?
                (double?)Math.Round((double)output.FieldGoalsMade / (double)output.FieldGoalsAttempted, 3) : null;

            output.ThreePointsFieldGoalPercentage = (output.ThreePointFieldGoalsAttempted > 0) ?
                (double?)Math.Round((double)output.ThreePointFieldGoalsMade / (double)output.ThreePointFieldGoalsAttempted, 3) : null;

            output.FreeThrowPercentage = (output.FreeThrowsAttempted > 0) ?
                (double?)Math.Round((double)output.FreeThrowsMade / (double)output.FreeThrowsAttempted, 3) : null;

            return output;
        }

        private static int? AddValueToTotal(int? outputVal, int? profileVal)
        {
            if (profileVal != null)
            {
                if (outputVal == null) outputVal = 0;
                return outputVal += profileVal;
            }
            return null;
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
