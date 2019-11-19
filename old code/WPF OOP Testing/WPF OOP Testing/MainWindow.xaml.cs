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
using WPF_OOP_Testing.Fenster;

namespace WPF_OOP_Testing
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void AlterButton_Click(object sender, RoutedEventArgs e)
        {
            AlterItemsPage aip = new AlterItemsPage();
            aip.Show();
        }

        private void CheckStatsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CustomerViewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutMenu_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            CloseAllWindowsButMain();
            LogInPage lgin = new LogInPage();
            lgin.Show();
            this.Close();
        }
        private void CloseAllWindowsButMain()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 1; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
