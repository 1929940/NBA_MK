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

            BindControls(selectedPlayerID);

            PlayerView_Label.Content = selectedPlayersName;

            Seasons_CBX.SelectedValue = selectedSeason;

            Teams_CBX.SelectedValue = selectedTeamsName;

            Console.WriteLine(selectedPlayerID);
            Console.WriteLine(selectedPlayersName);
            Console.WriteLine(selectedTeamsName);
        }

        private async Task BindControls(int id)
        {
            playerProfiles = await JsonReader.GetPlayerProfile(id);

            Seasons_CBX.ItemsSource = PlayerProfile.GetSeasons(playerProfiles);

            Teams_CBX.ItemsSource = PlayerProfile.GetTeamIDs(playerProfiles);
        }
        private void BindStats(string season)
        {
            PlayerProfile profile;

            profile = playerProfiles.FirstOrDefault(p => p.SeasonID == season) ?? playerProfiles[playerProfiles.Count-1];

            GP_LBL.Content = profile.GamesPlayed;
            GS_LBL.Content = profile.GamesStarted;
            Min_LBL.Content = profile.MinutesPlayed;

            FGM_LBL.Content = profile.FieldGoalsMade;
            FGA_LBL.Content = profile.FieldGoalsAttempted;
            FGP_LBL.Content = profile.FieldGoalsPercentage;

            TPM_LBL.Content = profile.FreeThrowsMade;
            TPA_LBL.Content = profile.FreeThrowsAttempted;
            TPP_LBL.Content = profile.FreeThrowPercentage;

            FTM_LBL.Content = profile.FreeThrowsMade;
            FTA_LBL.Content = profile.FreeThrowsAttempted;
            FTP_LBL.Content = profile.FreeThrowPercentage;

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
            BindStats(Seasons_CBX.SelectedValue.ToString());
        }
    }
}
