using NBA_Lib.JsonReader;
using NBA_Lib.Model;
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
    public partial class TeamWindow : Window
    {
        readonly int teamID;
        readonly string teamName;
        readonly List<Franchise> franchiseData;
        CollectionView collectionView;

        public TeamWindow(Team team, List<string> seasons, string selectedSeason, List<Franchise> franchises)
        {
            InitializeComponent();

            teamID = team.TeamID;
            teamName = team.TeamName;

            franchiseData = franchises;

            MainLabel.Content = teamName + " Team Rooster";

            BindSeasons(seasons, selectedSeason);
        }
        private async Task BindRooster(int teamID, string season)
        {
            var rooster = await JsonReader.GetTeamRosterAsync(teamID, season);

            TeamRoosterGrid.ItemsSource = rooster;

            collectionView = (CollectionView)CollectionViewSource.GetDefaultView(TeamRoosterGrid.ItemsSource);

            collectionView.Filter = MyFilter;
        }
        private bool MyFilter(object item)
        {
            return FilterByName(item) && FilterByPosition(item) && FilterByRole(item);
        }

        private bool FilterByName(object item)
        {
            if (String.IsNullOrEmpty(TeamFilter.Text))
                return true;
            else
                return ((item as TeamRooster).PlayerName.IndexOf(TeamFilter.Text,
                    StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool FilterByRole(object item)
        {
            string selectedVal = Role_CBX.SelectedValue?.ToString().Substring(38);

            if (selectedVal == "All")
                return true;
            else
                return ((item as TeamRooster).Role == selectedVal);
        }
        private bool FilterByPosition(object item)
        {
            string selectedVal = Position_CBX.SelectedValue?.ToString().Substring(38);

            if (selectedVal == "All")
                return true;
            else
            {
                var splitted = selectedVal.Split('-');

                if (splitted.Length > 1)
                {
                    return ((item as TeamRooster).Position.Contains(splitted[0])
                        && (item as TeamRooster).Position.Contains(splitted[1]));
                }

                return ((item as TeamRooster).Position == splitted[0]);
            }
        }

        private void BindSeasons(List<string> seasons, string season)
        {
            Seasons_CBX.ItemsSource = seasons;

            Seasons_CBX.SelectedValue = season;
        }

        private void TeamRoosterGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((TeamRoosterGrid.SelectedItem as TeamRooster).Role == "Coach")
            {
                MessageBox.Show("There is no additional data for coaches. \nTry choosing a player.", "Coaches and Trainers");
                return;
            }

            int playerID = (int)(TeamRoosterGrid.SelectedItem as TeamRooster).PlayerID;

            string playerName = (TeamRoosterGrid.SelectedItem as TeamRooster).PlayerName;

            string selectedSeason = Seasons_CBX.SelectedValue.ToString();

            PlayerWindow playerWindow = new PlayerWindow(playerID, playerName, teamID, selectedSeason, franchiseData);
            playerWindow.ShowDialog();
        }

        private void Seasons_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindRooster(teamID, Seasons_CBX.SelectedValue.ToString());
        }

        private void CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamRoosterGrid?.ItemsSource != null)
                CollectionViewSource.GetDefaultView(TeamRoosterGrid.ItemsSource).Refresh();
        }

        private void TeamFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(TeamRoosterGrid.ItemsSource).Refresh();
        }
    }
}
