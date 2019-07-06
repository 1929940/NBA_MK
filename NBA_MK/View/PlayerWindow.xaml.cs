using NBA_Lib.JsonReader;
using NBA_Lib.JsonReader.JsonObjects;
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
        List<PlayerProfile> playerProfiles;
        public bool FirstInitialization { get; set; } = true;
        public string SelectedSeason { get; set; }

        public PlayerWindow(int selectedPlayerID, string selectedPlayersName, int teamID, string selectedSeason, List<Franchise> franchises)
        {
            InitializeComponent();

            SelectedSeason = selectedSeason;

            BindControls(selectedPlayerID, franchises, teamID);

            PlayerView_Label.Content = selectedPlayersName;
        }

        private async Task BindControls(int id, List<Franchise> franchises, int teamID)
        {
            playerProfiles = await JsonReader.GetPlayerProfile(id);

            Teams_CBX.ItemsSource = PlayerProfile.GetTeamIdNameDictionary(playerProfiles, franchises);

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

        private void UpdateLabels(string season, int id)
        {
            PlayerProfile profile;

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

            if (id != -1)
            {
                profile = playerProfiles.Where(p => p.TeamID == id).FirstOrDefault(p => p.SeasonID == season) 
                    ?? PlayerProfile.GetPlayersTotalStatsWhileInTeam(id, playerProfiles);
            }
            else
            {
                profile = playerProfiles.Where(p => p.TeamID == 0).FirstOrDefault(p => p.SeasonID == season)
                    ?? playerProfiles.FirstOrDefault(p => p.SeasonID == season)
                    ?? playerProfiles[playerProfiles.Count-1];
            }

            GP_LBL.Content = (profile.GamesPlayed == -100) ? 
                "No Data" : profile.GamesPlayed.ToString();
            GS_LBL.Content = (profile.GamesStarted == -100) ? 
                "No Data" : profile.GamesStarted.ToString();
            Min_LBL.Content = (profile.MinutesPlayed == -100) ? 
                "No Data" : profile.MinutesPlayed.ToString();

            FGM_LBL.Content = (profile.FieldGoalsMade == -100) ? 
                "No Data" : profile.FieldGoalsMade.ToString();
            FGA_LBL.Content = (profile.FieldGoalsAttempted == -100) ? 
                "No Data" : profile.FieldGoalsAttempted.ToString();
            FGP_LBL.Content = (profile.FieldGoalsPercentage == -100) ? 
                "No Data" : profile.FieldGoalsPercentage * 100 + "%";

            TPM_LBL.Content = (profile.ThreePointFieldGoalsMade == -100) ? 
                "No Data" : profile.ThreePointFieldGoalsMade.ToString();
            TPA_LBL.Content = (profile.ThreePointFieldGoalsAttempted == -100) ? 
                "No Data" : profile.ThreePointFieldGoalsAttempted.ToString(); ;
            TPP_LBL.Content = (profile.ThreePointsFieldGoalPercentage == -100) ? 
                "No Data" : profile.ThreePointsFieldGoalPercentage * 100 + "%";

            FTM_LBL.Content = (profile.FreeThrowsMade == -100) ? 
                "No Data" : profile.FreeThrowsMade.ToString();
            FTA_LBL.Content = (profile.FreeThrowsAttempted == -100) ? 
                "No Data" : profile.FreeThrowsAttempted.ToString(); ;
            FTP_LBL.Content = (profile.FreeThrowPercentage == -100) ? 
                "No Data" : profile.FreeThrowPercentage * 100 + "%";

            OREB_LBL.Content = (profile.OffensiveRebounds == -100) ? 
                "No Data" : profile.OffensiveRebounds.ToString();
            DREB_LBL.Content = (profile.DefensiveRebounds == -100) ? 
                "No Data" : profile.DefensiveRebounds.ToString();
            Reb_LBL.Content = (profile.Rebounds == -100) ? 
                "No Data" : profile.Rebounds.ToString();

            PTS_LBL.Content = (profile.Points == -100) ? 
                "No Data" : profile.Points.ToString();
            Ast_LBL.Content = (profile.Assists == -100) ? 
                "No Data" : profile.Assists.ToString();
            Stl_LBL.Content = (profile.Steals == -100) ? 
                "No Data" : profile.Steals.ToString();
            Blc_LBL.Content = (profile.Blocks == -100) ? 
                "No Data" : profile.Blocks.ToString();
            Tov_LBL.Content = (profile.Turnover == -100) ? 
                "No Data" : profile.Turnover.ToString();
            PF_LBL.Content = (profile.PersonalFouls == -100) ? 
                "No Data" : profile.PersonalFouls.ToString();
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

            var list = PlayerProfile.GetSeasons(playerProfiles, key);

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
