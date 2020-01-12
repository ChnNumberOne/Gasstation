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
        // gas station object
        private Tankstelle tankstelle;

        // fuel tank selected by user in GUI
        private FuelTank selectedFuelTank;

        public AlterItems()
        {
            InitializeComponent();
            RefreshPage();
        }

        // refreshes all data in page
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

        // for updating GUI on fuel tank selection
        private void SelectFuelTank(FuelTank fuelTank)
        {
            // setting values of the selected fueltank
            SelectedFuelTankLabel.Content = fuelTank.GetFuelType().GetFuelTypeName() + " Tank";
            FuelTankFilling.Content = fuelTank.GetCurrentFuelAmount().ToString() + "/" + fuelTank.GetMaxCapacity().ToString() + "L";
            FuelTankPercentage.Content = fuelTank.GetFillPercentage().ToString("n2") + "%";
            selectedFuelTank = fuelTank;
        }

        // for checking if textbox input is valid
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

        // fill fuel into selected fuel tank
        private void FillTankButton_Click(object sender, RoutedEventArgs e)
        {
            int amountOfFuel;
            if (!string.IsNullOrEmpty(FuelTankBox.Text) && int.TryParse(FuelTankBox.Text, out amountOfFuel) && !FuelTankBox.Text.Contains('-') && selectedFuelTank != null)
            {
                selectedFuelTank.AddFuelToTank(amountOfFuel);
                tankstelle.SaveFuelTanks();
                SelectFuelTank(selectedFuelTank);
                FuelTankBox.Text = "";
            }
        }

        // drain fuel from selected fuel tank
        private void EmptyFuel_Click(object sender, RoutedEventArgs e)
        {
            int amountOfFuel;
            if (!string.IsNullOrEmpty(FuelTankBox.Text) && int.TryParse(FuelTankBox.Text, out amountOfFuel) && !FuelTankBox.Text.Contains('-') && selectedFuelTank != null)
            {
                selectedFuelTank.DrainFuel(amountOfFuel);
                tankstelle.SaveFuelTanks();
                SelectFuelTank(selectedFuelTank);
                FuelTankBox.Text = "";
            }
        }
    }
}
