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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        private bool isOnStats = false;
        private bool isOnAlterItems = false;

        public WelcomePage()
        {
            InitializeComponent();
            QuickDisplayFrame.Content = new DisplayWelcome();
        }

        private void CustomerViewButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new CustomerSimulation());
        }

        private void CheckStatsButton_Click(object sender, RoutedEventArgs e)
        {
            if ((!isOnStats && !isOnAlterItems) || isOnAlterItems)
            {
                QuickDisplayFrame.Content = new Statistics();
                isOnStats = true;
                isOnAlterItems = false;
            }
            else
            {
                QuickDisplayFrame.Content = new DisplayWelcome();
                isOnStats = false;
                isOnAlterItems = false;
            }
        }

        private void AlterButton_Click(object sender, RoutedEventArgs e)
        {
            if ((!isOnStats && !isOnAlterItems) || isOnStats)
            {
                QuickDisplayFrame.Content = new AlterItems();
                isOnStats = false;
                isOnAlterItems = true;
            }
            else
            {
                QuickDisplayFrame.Content = new DisplayWelcome();
                isOnStats = false;
                isOnAlterItems = false;
            }
        }
    }
}
