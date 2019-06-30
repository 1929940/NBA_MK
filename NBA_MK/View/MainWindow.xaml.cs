﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BindTeams();
            BindTeams();

        }

        private async void BindTeams()
        {
            var teams = await NBA_Lib.JsonReader.JsonReader.GetTeamsAsync();

            TeamGridWest.ItemsSource = teams.Where(t => t.Conference == "West");
            TeamGridEast.ItemsSource = teams.Where(t => t.Conference == "East");


        }
    }
}
