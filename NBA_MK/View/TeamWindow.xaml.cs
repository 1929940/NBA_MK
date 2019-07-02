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
        //string season;
        int teamID;
        public TeamWindow(string season, Team team, List<string> seasons)
        {
            InitializeComponent();

            //season = this.season;
            teamID = team.TeamID;

            //Blad logiczny? Filter by Season, why? Nie. Zostaje tak jak jest w planie.

            MainLabel.Content = team.TeamName + " Team Rooster";

            //BindRooster(team.TeamID, season);

            BindSeasons(seasons, season);
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
            PlayerWindow playerWindow = new PlayerWindow();
            playerWindow.ShowDialog();
        }

        private void Seasons_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(Seasons_CBX.SelectedValue);

            BindRooster(teamID, Seasons_CBX.SelectedValue.ToString());
        }
    }
}
