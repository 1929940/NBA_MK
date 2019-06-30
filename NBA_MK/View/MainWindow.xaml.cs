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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BindSeasons();


        }

        public async Task BindTeams(string season)
        {
            var teams = await JsonReader.GetTeamsAsync(season);

            TeamGridWest.ItemsSource = teams.Where(t => t.Conference == "West");
            TeamGridEast.ItemsSource = teams.Where(t => t.Conference == "East");
        }
        private async Task BindSeasons()
        {
            var teamSeasons = await JsonReader.GetTeamSeasonsAsync();

            var season = TeamSeasons.GetSeasonsList(teamSeasons);

            SeasonsGrid.ItemsSource = season;

            SeasonsGrid.SelectedIndex = 0;
        }

        private void SeasonsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(SeasonsGrid.SelectedItem.ToString());

            BindTeams(SeasonsGrid.SelectedItem.ToString());

        }
    }
}
