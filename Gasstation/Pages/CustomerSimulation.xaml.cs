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

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für CustomerSimulation.xaml
    /// </summary>
    public partial class CustomerSimulation : Page
    {



        // Bugtracker

        // if the Fueltype is selected and a number is put in. Changing the fueltype will not update the price calculated.
        // diverse null pointers


        public CustomerSimulation()
        {
            InitializeComponent();
            RefreshPage();
            
        }

        private Zapfhahn selectedZapfhahn;

        private int userLiterAmount;

        private void DisplayTotalFuelValue()
        {
            if(selectedZapfhahn != null) { 
                CostBox.Text = this.userLiterAmount * (float)selectedZapfhahn.GetFuelTank().GetFuelType().GetCostPerLiterInCent() / 100 + ".-";
            }
        }

        private void RefreshPage()
        {


            ZapfsaeulenPanel.Children.Clear();
            ZapfhahnPanel.Children.Clear();

            
            int i = 0;
            foreach (Zapfsaeule zapfsaeule in GasstationState.AvailableZapfsaeulen)
            {
                i++;
                Button zapfsaeuleButton = new Button()
                {
                    Content = i.ToString(),
                    Margin = new Thickness(0, 1, 0, 1)
                };
                zapfsaeuleButton.Click += (s, e) => 
                {
                    ZapfhahnPanel.Children.Clear();
              
                    
                    foreach (Zapfhahn zapfhahn in zapfsaeule.GetZapfhaene())
                    {

                        // Auswahl Treibstoff
                        Button zapfhahnButton = new Button()
                        {
                            // TODO: bad Practice ( Better idea?)
                            Content = zapfhahn.GetFuelTank().GetFuelType().GetFuelTypeName(),
                            MinWidth = 50,
                            Margin = new Thickness(1)
                        };
                        zapfhahnButton.Click += (sZapfhahn, eZapfhahn) =>
                        {
                            
                            zapfsaeule.Selectzapfhahn(zapfhahn);

                            // TODO: bad Practie look at this with f
                            this.selectedZapfhahn = zapfhahn;
                         
                            SelectedFuelLabel.Content = zapfhahn.GetFuelTank().GetFuelType().GetFuelTypeName();
                            CostPerLiterTextBlock.Text = ((float)zapfhahn.GetFuelTank().GetFuelType().GetCostPerLiterInCent()/ 100).ToString();
                            DisplayTotalFuelValue();
                           
                           
                        };
                        ZapfhahnPanel.Children.Add(zapfhahnButton);

                        // Treibstoff Beziehen
                            



                        // Treibstoff Bezug senden

                    }
                };
                ZapfsaeulenPanel.Children.Add(zapfsaeuleButton);
            }
        }

        private void ZapfsaeuleButton_Click(object sender, RoutedEventArgs e)
        {
         
        }

            private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

        private void FuelToTakeOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Textfeld zum Ausgabe zu machen.

            if (int.TryParse(FuelToTakeOut.Text, out this.userLiterAmount)) 
            {
                ErrorBlock.Text = "";

                // Just a simple test for calculating the cost per liter
                DisplayTotalFuelValue();
            }
            else if (string.IsNullOrEmpty(FuelToTakeOut.Text)) 
            {
                ErrorBlock.Text = "";
                CostBox.Text = "";
            }
            else 
            {
                ErrorBlock.Text = "Input is invalid!";
                CostBox.Text = "";
            }
        }

        private void TakeFuel_Click(object sender, RoutedEventArgs e)
        {
            if(this.selectedZapfhahn != null) { 
            this.selectedZapfhahn.DrainFuelFromTank(this.userLiterAmount);
            Console.WriteLine(this.selectedZapfhahn.GetFuelTank().GetFillPercentage());
            }
        }
    }
}
