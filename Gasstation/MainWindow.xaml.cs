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
using Gasstation.Implementation;
using Gasstation.Pages;

namespace Gasstation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // static frame for changing content from other pages
        private static Frame mainFrame;

        // constructor
        public MainWindow()
        {
            InitializeComponent();
            MainMenuControl.Content = new WelcomePage();
            mainFrame = MainMenuControl;
        }

        // static function for setting mainFrame
        public static void SetContent(object newPage)
        {
            mainFrame.Content = newPage;
        }
    }
}
