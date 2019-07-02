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
