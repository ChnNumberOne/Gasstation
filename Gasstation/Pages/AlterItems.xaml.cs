using Gasstation.Implementation;
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
    /// Interaktionslogik für AlterItems.xaml
    /// </summary>
    public partial class AlterItems : Page
    {
        private Tankstelle tankstelle;

        public AlterItems()
        {
            InitializeComponent();
            RefreshPage();
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

        private void RefreshPage()
        {
            // initialize singleton
            this.tankstelle = Tankstelle.Current();


            // Clear GUI
            FuelTankPanel.Children.Clear();

            // Load Again
            foreach (FuelTank fuelTank in tankstelle.GetAvailableFuelTanks())
            {
                Button fuelTankButton = new Button()
                {
                    Content = fuelTank.GetFuelType().GetFuelTypeName() + "Tank",
                    Margin = new Thickness(0, 1, 0, 1)
                };
                // Set Function on Button
                fuelTankButton.Click += (s, e) => SelectFuelTank(fuelTank);
                FuelTankPanel.Children.Add(fuelTankButton);
            }
        }

        private void SelectFuelTank(FuelTank fuelTank)
        {

        }
    }
}
