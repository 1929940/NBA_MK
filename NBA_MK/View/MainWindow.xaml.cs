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
using NBA_Lib.Model;

namespace NBA_MK.View
{
    public partial class MainWindow : Window
    {
        List<TeamData> teamData;
        public MainWindow()
        {
            InitializeComponent();

            BindSeasons();
        }


        public async Task BindTeams(string season)
        {
            var teams = (await JsonReader.GetTeamsAsync(season)).ExtractTeamStats(season);

            TeamGridWest.ItemsSource = TeamStats.CorrectStanding(teams.Where(t => t.Conference == "West"));
            TeamGridEast.ItemsSource = TeamStats.CorrectStanding(teams.Where(t => t.Conference == "East"));
        }
        private async Task BindSeasons()
        {
            teamData = (await JsonReader.GetFranchiseDataAsync()).ExtractTeamData();

            var season = TeamData.GetActiveSeasons(teamData);

            SeasonsGrid.ItemsSource = season;

            SeasonsGrid.SelectedIndex = 0;
        }

        private void SeasonsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindTeams(SeasonsGrid.SelectedItem.ToString());
        }

        private void TeamGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TeamStats team;
            string selectedSeason = SeasonsGrid.SelectedItem.ToString();

            if ((sender as DataGrid).Name == "TeamGridWest")
            {
                team = (TeamGridWest.SelectedItem as TeamStats);
            }
            else
            {
                team = (TeamGridEast.SelectedItem as TeamStats);
            }

            List<string> selectedTeamsSeasons = 
                TeamData.GetActiveSeasons(teamData.Where(p => p.TeamID == team.TeamID).ToList());

            TeamWindow teamWindow = new TeamWindow(team, selectedTeamsSeasons, selectedSeason, teamData );
            teamWindow.ShowDialog();
        }
    }
}
