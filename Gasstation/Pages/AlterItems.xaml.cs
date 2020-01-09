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
        private FuelTank selectedFuelTank;

        public AlterItems()
        {
            InitializeComponent();
            RefreshPage();
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
                    Content = fuelTank.GetFuelType().GetFuelTypeName() + " Tank",
                    Margin = new Thickness(0, 1, 0, 1)
                };
                // Set Function on Button
                fuelTankButton.Click += (s, e) => SelectFuelTank(fuelTank);
                FuelTankPanel.Children.Add(fuelTankButton);
            }
        }

        private void SelectFuelTank(FuelTank fuelTank)
        {
            // setting values of the selected fueltank
            SelectedFuelTankLabel.Content = fuelTank.GetFuelType().GetFuelTypeName() + " Tank";
            FuelTankFilling.Content = fuelTank.GetCurrentFuelAmount().ToString() + "/" + fuelTank.GetMaxCapacity().ToString() + "L";
            FuelTankPercentage.Content = fuelTank.GetFillPercentage().ToString("n2") + "%";
            selectedFuelTank = fuelTank;
        }

        private void FuelTankBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ignoreInt;
            if (!int.TryParse(FuelTankBox.Text, out ignoreInt) && !string.IsNullOrEmpty(FuelTankBox.Text) || FuelTankBox.Text.Contains('-'))
            {
                FuelTankBox.Foreground = Brushes.Red;
            }
            else
            {
                FuelTankBox.Foreground = Brushes.Black;
            }
        }

        private void FillTankButton_Click(object sender, RoutedEventArgs e)
        {
            int amountOfFuel;
            if (!string.IsNullOrEmpty(FuelTankBox.Text) && int.TryParse(FuelTankBox.Text, out amountOfFuel) && !FuelTankBox.Text.Contains('-') && selectedFuelTank != null)
            {
                selectedFuelTank.AddFuelToTank(amountOfFuel);
                SelectFuelTank(selectedFuelTank);
                FuelTankBox.Text = "";
            }
        }

        private void EmptyFuel_Click(object sender, RoutedEventArgs e)
        {
            int amountOfFuel;
            if (!string.IsNullOrEmpty(FuelTankBox.Text) && int.TryParse(FuelTankBox.Text, out amountOfFuel) && !FuelTankBox.Text.Contains('-') && selectedFuelTank != null)
            {
                selectedFuelTank.DrainFuel(amountOfFuel);
                SelectFuelTank(selectedFuelTank);
                FuelTankBox.Text = "";
            }
        }
    }
}
