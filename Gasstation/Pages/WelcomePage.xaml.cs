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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        private bool isOnWelcome = true;

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
            if (isOnWelcome)
            {
                QuickDisplayFrame.Content = new Statistics();
                isOnWelcome = false;
            }
            else
            {
                QuickDisplayFrame.Content = new DisplayWelcome();
                isOnWelcome = true;
            }
        }

        private void AlterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new AlterItems());
        }
    }
}
