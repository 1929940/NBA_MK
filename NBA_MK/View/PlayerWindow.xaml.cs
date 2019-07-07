using NBA_Lib.JsonReader;
using NBA_Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NBA_MK.View
{
    public partial class PlayerWindow : Window
    {
        List<PlayerStats> playerStats;
        public bool FirstInitialization { get; set; } = true;
        public string SelectedSeason { get; set; }

        public PlayerWindow(int selectedPlayerID, string selectedPlayersName, int teamID, string selectedSeason, List<TeamData> teamData)
        {
            InitializeComponent();

            SelectedSeason = selectedSeason;

            BindControls(selectedPlayerID, teamData, teamID);

            PlayerView_Label.Content = selectedPlayersName;
        }

        private async Task BindControls(int id, List<TeamData> teamData, int teamID)
        {
            playerStats = (await JsonReader.GetPlayerProfile(id)).ExtractPlayerStats();

            Teams_CBX.ItemsSource = PlayerStats.GetTeamIdNameDictionary(playerStats, teamData);

            SetSelectedItem(Teams_CBX, teamID);
        }
        private void SetSelectedItem(ComboBox cbx, int teamID)
        {
            foreach (var item in cbx.Items)
            {
                int key = ((KeyValuePair<int, string>)item).Key;
                if (key == teamID)
                {
                    Teams_CBX.SelectedItem = item;
                    return;
                }
            }

            //Fixes a rare bug caused by inconsistencies in the results of CommonTeamRoster and PlayerProfile API
            Teams_CBX.SelectedIndex = Teams_CBX.Items.Count - 1;
            Seasons_CBX.SelectedIndex = Seasons_CBX.Items.Count - 1;
        }

        private void UpdateLabels(string season, int teamId)
        {
            PlayerStats profile;

            /**Based on team and season comboboxes, determines which playerprofile will be displayed.
            Depending on the teamID, there are 2 possible paths:

            One: ID is -1.

            Means the user has chosen to display[all teams] in TeamComboBox
           In the case of the player being a part of more than one team in a season
            playerProfiles with teamid of 0 contain data for the whole season including all teams
            When[all teams] and[all seasons] are displayed the last entry in playerProfiles is chosen.
            It constains the total data of a players statistics through his career.

            Two: ID > 0.

            Means the user has chosen a specific team,
            In case of chosing to display[all seasons] a profile will be generated
            containing the sum of all statistic(eg.total minutes played in Chicago Bulls) from that team.**/

            if (teamId != -1)
            {
                profile = playerStats.Where(p => p.TeamID == teamId).FirstOrDefault(p => p.SeasonID == season) 
                    ?? PlayerStats.GetPlayersTotalStatsWhileInTeam(teamId, playerStats);
            }
            else
            {
                profile = playerStats.Where(p => p.TeamID == 0).FirstOrDefault(p => p.SeasonID == season)
                    ?? playerStats.FirstOrDefault(p => p.SeasonID == season)
                    ?? playerStats[playerStats.Count-1];
            }

            GP_LBL.Content = (profile.GamesPlayed == -100) ? 
                "Unavailable" : profile.GamesPlayed.ToString();
            GS_LBL.Content = (profile.GamesStarted == -100) ? 
                "Unavailable" : profile.GamesStarted.ToString();
            Min_LBL.Content = (profile.MinutesPlayed == -100) ? 
                "Unavailable" : profile.MinutesPlayed.ToString();

            FGM_LBL.Content = (profile.FieldGoalsMade == -100) ? 
                "Unavailable" : profile.FieldGoalsMade.ToString();
            FGA_LBL.Content = (profile.FieldGoalsAttempted == -100) ? 
                "Unavailable" : profile.FieldGoalsAttempted.ToString();
            FGP_LBL.Content = (profile.FieldGoalsPercentage == -100) ? 
                "Unavailable" : profile.FieldGoalsPercentage * 100 + "%";

            TPM_LBL.Content = (profile.ThreePointFieldGoalsMade == -100) ? 
                "Unavailable" : profile.ThreePointFieldGoalsMade.ToString();
            TPA_LBL.Content = (profile.ThreePointFieldGoalsAttempted == -100) ? 
                "Unavailable" : profile.ThreePointFieldGoalsAttempted.ToString(); ;
            TPP_LBL.Content = (profile.ThreePointsFieldGoalPercentage == -100) ? 
                "Unavailable" : profile.ThreePointsFieldGoalPercentage * 100 + "%";

            FTM_LBL.Content = (profile.FreeThrowsMade == -100) ? 
                "Unavailable" : profile.FreeThrowsMade.ToString();
            FTA_LBL.Content = (profile.FreeThrowsAttempted == -100) ? 
                "Unavailable" : profile.FreeThrowsAttempted.ToString(); ;
            FTP_LBL.Content = (profile.FreeThrowPercentage == -100) ? 
                "Unavailable" : profile.FreeThrowPercentage * 100 + "%";

            OREB_LBL.Content = (profile.OffensiveRebounds == -100) ? 
                "Unavailable" : profile.OffensiveRebounds.ToString();
            DREB_LBL.Content = (profile.DefensiveRebounds == -100) ? 
                "Unavailable" : profile.DefensiveRebounds.ToString();
            Reb_LBL.Content = (profile.Rebounds == -100) ? 
                "Unavailable" : profile.Rebounds.ToString();

            PTS_LBL.Content = (profile.Points == -100) ? 
                "Unavailable" : profile.Points.ToString();
            Ast_LBL.Content = (profile.Assists == -100) ? 
                "Unavailable" : profile.Assists.ToString();
            Stl_LBL.Content = (profile.Steals == -100) ? 
                "Unavailable" : profile.Steals.ToString();
            Blc_LBL.Content = (profile.Blocks == -100) ? 
                "Unavailable" : profile.Blocks.ToString();
            Tov_LBL.Content = (profile.Turnover == -100) ? 
                "Unavailable" : profile.Turnover.ToString();
            PF_LBL.Content = (profile.PersonalFouls == -100) ? 
                "Unavailable" : profile.PersonalFouls.ToString();
        }

        private void Seasons_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string season = Seasons_CBX?.SelectedValue?.ToString();

            int teamId = ((KeyValuePair<int, string>)Teams_CBX.SelectedValue).Key;

            UpdateLabels(season, teamId);
        }

        private void Teams_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int key = ((KeyValuePair<int, string>)Teams_CBX.SelectedValue).Key;

            var list = PlayerStats.GetSeasons(playerStats, key);

            Seasons_CBX.ItemsSource = list;

            if (FirstInitialization)
            {
                Seasons_CBX.SelectedValue = SelectedSeason;
                FirstInitialization = false;
            }
            else
            {
                //Ensures Seasons_CBX_SelectionChanged event is fired up.
                Seasons_CBX.SelectedIndex = -1;
                Seasons_CBX.SelectedIndex = Seasons_CBX.Items.Count - 1;
            }
        }
    }
}
