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
using Gasstation.Testfolder;

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

            // Test für Konfigurationsschreiben (muss nicht hier sein)
            FuelType testFuelType = new FuelType("Benzin", 125);
            FuelType testFuelType2 = new FuelType("Diesel", 135);
            FuelType testFuelType3 = new FuelType("Super", 130);
            List<FuelType> testFuelTypes = new List<FuelType>();
            testFuelTypes.Add(testFuelType);
            testFuelTypes.Add(testFuelType2);
            testFuelTypes.Add(testFuelType3);
            TankstellenKonfiguration config = new TankstellenKonfiguration(testFuelTypes, 3);
            config.SaveConfig();





            FuelTank newCreatedFuelTank = new FuelTank(new FuelType("Benzin",125), 1000);
            Button eventButton = (Button) e.Source;
            StackPanel stackPanel = (StackPanel) eventButton.Parent;

            Button newTankButton = new Button();
            newTankButton.Content = "Tank " + (counter++);
            newTankButton.Click += (s, events) => {
                newCreatedFuelTank.AddFuelToTank(100);
                Console.WriteLine(newCreatedFuelTank.GetFillPercentage());
                newCreatedFuelTank.DrainFuelFromTank(50);
                Console.WriteLine(newCreatedFuelTank.GetFillPercentage());

            };
            stackPanel.Children.Add(newTankButton);
            

        }
     

    }
}
