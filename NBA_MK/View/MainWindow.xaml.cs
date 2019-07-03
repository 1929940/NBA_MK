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
        //List<Team> teams;
        List<Franchise> franchiseData;
        public MainWindow()
        {
            InitializeComponent();

            BindSeasons();

            testFranchise();

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
        public async Task testFranchise()
        {
            var test = await JsonReader.GetFranchiseDataAsync();

            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }

        public async Task BindTeams(string season)
        {
            var teams = await JsonReader.GetTeamsAsync(season);

            TeamGridWest.ItemsSource = teams.Where(t => t.Conference == "West");
            TeamGridEast.ItemsSource = teams.Where(t => t.Conference == "East");
        }
        private async Task BindSeasons()
        {
            franchiseData = await JsonReader.GetFranchiseDataAsync();

            var season = Franchise.GetActiveSeasonsList(franchiseData);

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
            string selectedSeason = SeasonsGrid.SelectedItem.ToString();

            if ((sender as DataGrid).Name == "TeamGridWest")
            {
                team = (TeamGridWest.SelectedItem as Team);
            }
            else
            {
                team = (TeamGridEast.SelectedItem as Team);
            }

            List<string> selectedTeamSeasons = 
                Franchise.GetActiveSeasonsList(franchiseData.Where(p => p.TeamID == team.TeamID).ToList());

            TeamWindow teamWindow = new TeamWindow(team, selectedTeamSeasons, selectedSeason, franchiseData );
            teamWindow.ShowDialog();
        }
    }
}
