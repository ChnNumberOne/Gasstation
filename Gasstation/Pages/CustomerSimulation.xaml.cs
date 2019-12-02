using System;
using System.Windows;
using System.Windows.Controls;
using Gasstation.Implementation;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für CustomerSimulation.xaml
    /// </summary>
    public partial class CustomerSimulation : Page
    {



        // Bugtracker

        // Zapfsaeulen Locken bisher aber unlocken nicht
        // Mit F Besprechen wegen Aufrufen in Programm von Objekten.



        public CustomerSimulation()
        {
            InitializeComponent();
            RefreshPage();
        }

        private Zapfhahn selectedZapfhahn;

        private Zapfsaeule selectedZapfsaeule;

        private int userLiterAmount;

        private Tankstelle tankstelle;

        private void DisplayTotalFuelValue(IFuelType fuelType)
        {
            CostBox.Text = this.userLiterAmount * (decimal)fuelType.GetCostPerLiterInCent() / 100 + ".-";
        }

        private void RefreshPage()
        {
            this.tankstelle = Tankstelle.Current();

            ZapfsaeulenPanel.Children.Clear();
            ZapfhahnPanel.Children.Clear();
            
            int i = 0;
            foreach (Zapfsaeule zapfsaeule in tankstelle.GetAllZapfsauelen())
            {
                i++;
                Button zapfsaeuleButton = new Button()
                {
                    Content = i.ToString(),
                    Margin = new Thickness(0, 1, 0, 1)
                };
                zapfsaeuleButton.Click += (s, e) => SelectZapfsauele(zapfsaeule);
                ZapfsaeulenPanel.Children.Add(zapfsaeuleButton);
            }
        }

        private void SelectZapfsauele(Zapfsaeule zapfsaeule)
        {
            this.selectedZapfsaeule = zapfsaeule;
            ZapfhahnPanel.Children.Clear();

            foreach (Zapfhahn zapfhahn in zapfsaeule.GetZapfhaene())
            {
                Button zapfhahnButton = new Button()
                {
                    // TODO: bad Practice ( Better idea?)
                    Content = zapfhahn.GetFuelType().GetFuelTypeName(),
                    MinWidth = 50,
                    Margin = new Thickness(1)
                };

                zapfhahnButton.Click += (s, e) => SelectZapfhahn(zapfhahn);                
                ZapfhahnPanel.Children.Add(zapfhahnButton);
            }
        }

        private void SelectZapfhahn(Zapfhahn zapfhahn)
        {
            this.selectedZapfhahn = zapfhahn;

            IFuelType fuelType = this.selectedZapfhahn.GetFuelType();
            SelectedFuelLabel.Content = fuelType.GetFuelTypeName();
            CostPerLiterTextBlock.Text = $"{(decimal)fuelType.GetCostPerLiterInCent() / 100}";
            DisplayTotalFuelValue(fuelType);
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
                DisplayTotalFuelValue(this.selectedZapfhahn.GetFuelType());
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


        // beide mit f besprechen. Bessere Variante??

        // muss warscheinlich in die Tankstelle static
        // Framework Methode
        private void TakeFuel_Click(object sender, RoutedEventArgs e)
        {
            if (selectedZapfsaeule != null)
            {
                this.tankstelle.PumpGasFromZapfsauele(this.selectedZapfsaeule, this.selectedZapfhahn.GetFuelType(), this.userLiterAmount);
                // display locked state
            }
            else
            {
                Console.WriteLine("Keine Zapfsaeule gewaehlt");
            }
        }
    }
}
