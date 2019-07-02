using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NBA_Lib.JsonReader;
using NBA_Lib.JsonReader.JsonObjects;

namespace NBA_MK.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<TeamSeasons> teamSeasons;
        public MainWindow()
        {
            InitializeComponent();

            BindSeasons();

            //testero();

            //JsonReader.GetPlayerProfile(2544);

            //testeroPlayero();

        }
        public async Task testero()
        {
            var test = await JsonReader.GetTeamRosterAsync(1610612739, "2018-19");

            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }
        public async Task testeroPlayero()
        {
            var test = await JsonReader.GetPlayerProfile(2544);

            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }


        public async Task BindTeams(string season)
        {
            // This will get passed down to player stats window (w3)
            // needed to convert teamid into teamname
            // Could cut this to have teamid/name but will pass down all for now
            var teams = await JsonReader.GetTeamsAsync(season);

            TeamGridWest.ItemsSource = teams.Where(t => t.Conference == "West");
            TeamGridEast.ItemsSource = teams.Where(t => t.Conference == "East");
        }
        private async Task BindSeasons()
        {
            // An single team season will get passed down based on
            // Team ID selected
            // This will get passed down to team stats (w2)
            // needed to show in what years the chosen team was active
            teamSeasons = await JsonReader.GetTeamSeasonsAsync();

            var season = TeamSeasons.GetSeasonsList(teamSeasons);

            SeasonsGrid.ItemsSource = season;

            SeasonsGrid.SelectedIndex = 0;
        }

        private void SeasonsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindTeams(SeasonsGrid.SelectedItem.ToString());
        }

        private void TeamGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Team team;

            if ((sender as DataGrid).Name == "TeamGridWest")
            {
                team = (Team)TeamGridWest.SelectedItem;
            }
            else
            {
                team = (Team)TeamGridEast.SelectedItem;
            }

            List<string> tmp = TeamSeasons.GetSeasonsList(teamSeasons.Where(p => p.TeamID == team.TeamID).ToList());

            TeamWindow teamWindow = new TeamWindow(SeasonsGrid.SelectedItem.ToString(), team, tmp);
            teamWindow.ShowDialog();
        }
    }
}
