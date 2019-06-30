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

            BindTeams();
            BindSeasons();

        }

        private async void BindTeams()
        {
            var teams = await JsonReader.GetTeamsAsync();

            TeamGridWest.ItemsSource = teams.Where(t => t.Conference == "West");
            TeamGridEast.ItemsSource = teams.Where(t => t.Conference == "East");
        }
        private async void BindSeasons()
        {
            var teamSeasons = await JsonReader.GetTeamSeasonsAsync();

            var season = TeamSeasons.GetSeasonsList(teamSeasons);

            SeasonsGrid.ItemsSource = season;

            // Grants me access to the first item in the collection
            Console.WriteLine(SeasonsGrid.Items[0]);
            //Console.WriteLine(SeasonsGrid.Columns[1].SetValue);


        }
    }
}
