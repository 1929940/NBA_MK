﻿using NBA_Lib.JsonReader;
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
        readonly List<TeamData> TeamData;
        CollectionView collectionView;

        public TeamWindow(TeamStats team, List<string> seasons, string selectedSeason, List<TeamData> teamData)
        {
            InitializeComponent();

            teamID = team.TeamID;
            teamName = team.TeamName;

            TeamData = teamData;

            MainLabel.Content = teamName + " Team Rooster";

            BindSeasons(seasons, selectedSeason);
        }
        private async Task BindRooster(int teamID, string season)
        {
            var rooster = (await JsonReader.GetTeamRosterAsync(teamID, season)).ExtractTeamMembers();

            TeamRoosterGrid.ItemsSource = rooster;

            collectionView = (CollectionView)CollectionViewSource.GetDefaultView(TeamRoosterGrid.ItemsSource);

            collectionView.Filter = MyFilter;
        }
        #region Filter
        private bool MyFilter(object item)
        {
            return FilterByName(item) && FilterByPosition(item) && FilterByRole(item);
        }

        private bool FilterByName(object item)
        {
            if (String.IsNullOrEmpty(TeamFilter.Text))
                return true;
            else
                return ((item as TeamMembers).PlayerName.IndexOf(TeamFilter.Text,
                    StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool FilterByRole(object item)
        {
            string selectedVal = Role_CBX.SelectedValue?.ToString().Substring(38);

            if (selectedVal == "All")
                return true;
            else
                return ((item as TeamMembers).Role == selectedVal);
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
                    return ((item as TeamMembers).Position.Contains(splitted[0])
                        && (item as TeamMembers).Position.Contains(splitted[1]));
                }

                return ((item as TeamMembers).Position == splitted[0]);
            }
        }
        #endregion

        private void BindSeasons(List<string> seasons, string season)
        {
            Seasons_CBX.ItemsSource = seasons;

            Seasons_CBX.SelectedValue = season;
        }

        private void TeamRoosterGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((TeamRoosterGrid.SelectedItem as TeamMembers).Role == "Coach")
            {
                MessageBox.Show("There is no additional data for coaches. \nTry choosing a player.", "Coaches and Trainers");
                return;
            }

            int playerID = (int)(TeamRoosterGrid.SelectedItem as TeamMembers).PlayerID;

            string playerName = (TeamRoosterGrid.SelectedItem as TeamMembers).PlayerName;

            string selectedSeason = Seasons_CBX.SelectedValue.ToString();

            PlayerWindow playerWindow = new PlayerWindow(playerID, playerName, teamID, selectedSeason, TeamData);
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
