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

            GP_LBL.Content  = profile.GamesPlayed?.ToString() ?? "Unavailable";
            GS_LBL.Content  = profile.GamesStarted?.ToString() ?? "Unavailable";
            Min_LBL.Content = profile.MinutesPlayed?.ToString() ?? "Unavailable";

            FGM_LBL.Content = profile.FieldGoalsMade?.ToString() ?? "Unavailable";
            FGA_LBL.Content = profile.FieldGoalsAttempted?.ToString() ?? "Unavailable";
            FGP_LBL.Content = (profile.FieldGoalsPercentage == null) 
                ? "Unavailable" : profile.FieldGoalsPercentage * 100 + "%";

            TPM_LBL.Content = profile.ThreePointFieldGoalsMade?.ToString() ?? "Unavailable";
            TPA_LBL.Content = profile.ThreePointFieldGoalsAttempted?.ToString() ?? "Unavailable"; ;
            TPP_LBL.Content = (profile.ThreePointsFieldGoalPercentage == null) 
                ? "Unavailable" : profile.ThreePointsFieldGoalPercentage * 100 + "%";

            FTM_LBL.Content = profile.FreeThrowsMade?.ToString() ?? "Unavailable";
            FTA_LBL.Content = profile.FreeThrowsAttempted?.ToString() ?? "Unavailable"; ;
            FTP_LBL.Content = (profile.FreeThrowPercentage == null) 
                ? "Unavailable" : profile.FreeThrowPercentage * 100 + "%";

            OREB_LBL.Content = profile.OffensiveRebounds?.ToString() ?? "Unavailable";
            DREB_LBL.Content = profile.DefensiveRebounds?.ToString() ?? "Unavailable";
            Reb_LBL.Content  = profile.Rebounds?.ToString() ?? "Unavailable";

            PTS_LBL.Content = profile.Points?.ToString() ?? "Unavailable";
            Ast_LBL.Content = profile.Assists?.ToString() ?? "Unavailable";
            Stl_LBL.Content = profile.Steals?.ToString() ?? "Unavailable";
            Blc_LBL.Content = profile.Blocks?.ToString() ?? "Unavailable";
            Tov_LBL.Content = profile.Turnover?.ToString() ?? "Unavailable";
            PF_LBL.Content = profile.PersonalFouls?.ToString() ?? "Unavailable";
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
