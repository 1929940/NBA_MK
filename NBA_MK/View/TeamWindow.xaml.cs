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
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        readonly int teamID;
        readonly string teamName;
        readonly List<Franchise> franchiseData;

        public TeamWindow(Team team, List<string> seasons, string selectedSeason, List<Franchise> franchises)
        {
            InitializeComponent();

            //season = this.season;
            teamID = team.TeamID;
            teamName = team.TeamName;

            franchiseData = franchises;

            MainLabel.Content = teamName + " Team Rooster";

            //BindRooster(team.TeamID, season);

            BindSeasons(seasons, selectedSeason);
        }
        private async Task BindRooster(int teamID, string season)
        {
            var rooster = await JsonReader.GetTeamRosterAsync(teamID, season);

            TeamRoosterGrid.ItemsSource = rooster;
        }
        private void BindSeasons(List<string> seasons, string season)
        {
            Seasons_CBX.ItemsSource = seasons;

            Seasons_CBX.SelectedValue = season;
        }

        private void TeamRoosterGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((TeamRoosterGrid.SelectedItem as TeamRooster).Role == "Coach") return;

            int playerID = (int)(TeamRoosterGrid.SelectedItem as TeamRooster).PlayerID;

            string playerName = (TeamRoosterGrid.SelectedItem as TeamRooster).PlayerName;

            string selectedSeason = Seasons_CBX.SelectedValue.ToString();

            PlayerWindow playerWindow = new PlayerWindow(playerID, playerName, teamName, selectedSeason, franchiseData);
            playerWindow.ShowDialog();
        }

        private void Seasons_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindRooster(teamID, Seasons_CBX.SelectedValue.ToString());
        }
    }
}
