using NBA_Lib.JsonReader;
using NBA_Lib.JsonReader.JsonObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NBA_MK.View
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        List<PlayerProfile> playerProfiles;

        public PlayerWindow(int selectedPlayerID, string selectedPlayersName, string selectedTeamsName, string selectedSeason, List<Franchise> franchises)
        {
            InitializeComponent();

            BindControls(selectedPlayerID, franchises, selectedSeason);

            PlayerView_Label.Content = selectedPlayersName;
        }

        private async Task BindControls(int id, List<Franchise> franchises, string selectedSeason)
        {
            playerProfiles = await JsonReader.GetPlayerProfile(id);

            Teams_CBX.ItemsSource = PlayerProfile.GetIdNameDictionary(playerProfiles, franchises);

            Teams_CBX.SelectedIndex = Teams_CBX.Items.Count - 1;
        }
        private void BindStats(string season, int id)
        {
            PlayerProfile profile;

            #region Explanation
            //Based on team and season comboboxes determines which playerprofile will be displayed 
            //Depending on the ID there are 2 possible paths

            //One: ID is 0. 

            //Means the user has chosen to display [all teams] in TeamComboBox
            //In the case of the player being a part of more than one team in a season
            //playerProfiles with teamid of -1 contain data for the whole season including all teams
            //When [all teams] and [all seasons] are displayed the last entry in playerProfiles is chosen.
            //It constains the total data of a players career

            //Two: ID > 0.

            //Means the user has chosen a specific team, 
            //In case of chosing to display [all seasons] a profile will be generated
            //containing the sum of all statistic(eg. total minutes played in Chicago Bulls) from that team.
            #endregion

            if (id != 0)
            {
                profile = playerProfiles.Where(p => p.TeamID == id).FirstOrDefault(p => p.SeasonID == season) 
                    ?? PlayerProfile.GetTotalStatsInTeam(id, playerProfiles);
            }
            else
            {
                profile = playerProfiles.Where(p => p.TeamID == -1).FirstOrDefault(p => p.SeasonID == season)
                    ?? playerProfiles.FirstOrDefault(p => p.SeasonID == season)
                    ?? playerProfiles[playerProfiles.Count-1];
            }

            GP_LBL.Content = profile.GamesPlayed;
            GS_LBL.Content = profile.GamesStarted;
            Min_LBL.Content = profile.MinutesPlayed;

            FGM_LBL.Content = profile.FieldGoalsMade;
            FGA_LBL.Content = profile.FieldGoalsAttempted;
            FGP_LBL.Content = profile.FieldGoalsPercentage * 100 + "%";

            TPM_LBL.Content = profile.ThreePointFieldGoalsMade;
            TPA_LBL.Content = profile.ThreePointFieldGoalsAttempted;
            TPP_LBL.Content = profile.ThreePointsFieldGoalPercentage * 100 + "%";

            FTM_LBL.Content = profile.FreeThrowsMade;
            FTA_LBL.Content = profile.FreeThrowsAttempted;
            FTP_LBL.Content = profile.FreeThrowPercentage * 100 + "%";

            OREB_LBL.Content = profile.OffensiveRebounds;
            DREB_LBL.Content = profile.DefensiveRebounds;
            Reb_LBL.Content = profile.Rebounds;

            PTS_LBL.Content = profile.Points;
            Ast_LBL.Content = profile.Assists;
            Stl_LBL.Content = profile.Steals;
            Blc_LBL.Content = profile.Blocks;
            Tov_LBL.Content = profile.Turnover;
            PF_LBL.Content = profile.PersonalFouls;
        }

        private void Seasons_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string season = Seasons_CBX?.SelectedValue?.ToString();

            int teamId = ((KeyValuePair<int, string>)Teams_CBX.SelectedValue).Key;

            BindStats(season, teamId);
        }

        private void Teams_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int key = ((KeyValuePair<int, string>)Teams_CBX.SelectedValue).Key;

            var list = PlayerProfile.GetSeasons(playerProfiles, key);

            Seasons_CBX.ItemsSource = list;

            Seasons_CBX.SelectedIndex = -1;
            Seasons_CBX.SelectedIndex = Seasons_CBX.Items.Count - 1;
        }
    }
}
