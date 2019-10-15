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

namespace Gasstation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int counter = 0;

        // Button Click Event 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FuelTank newCreatedFuelTank = new FuelTank(new FuelType(), 1000);
            Button eventButton = (Button) e.Source;
            StackPanel stackPanel = (StackPanel) eventButton.Parent;

            Button newTankButton = new Button();
            newTankButton.Content = "Tank " + (counter++);
            newTankButton.Click += (s, events) => {
                newCreatedFuelTank.AddFuelToTank(100);
                Console.WriteLine(newCreatedFuelTank.getFillPercentage());
                newCreatedFuelTank.DrainFuelFromTank(50);
                Console.WriteLine(newCreatedFuelTank.getFillPercentage());

            };
            stackPanel.Children.Add(newTankButton);
            

        }
    }
}
