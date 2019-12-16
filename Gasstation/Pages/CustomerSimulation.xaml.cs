using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Gasstation.Implementation;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für CustomerSimulation.xaml
    /// </summary>
    public partial class CustomerSimulation : Page
    {

        public CustomerSimulation()
        {
            
            InitializeComponent();
            RefreshPage();
        }

        private Zapfhahn selectedZapfhahn;

        private Zapfsaeule selectedZapfsaeule;

        private Tankstelle tankstelle;

        private void DisplayTotalFuelValue(IFuelType fuelType, int currentFuelTransaction)
        {
            CostBox.Text = currentFuelTransaction * (decimal)fuelType.GetCostPerLiterInCent() / 100 + ".-";
        }

        private void RefreshPage()
        {
            // Singleton Initalisieren
            this.tankstelle = Tankstelle.Current();

            // Clear GUI
            ZapfsaeulenPanel.Children.Clear();
            ZapfhahnPanel.Children.Clear();
            
            // Load Again
            int i = 0;
            foreach (Zapfsaeule zapfsaeule in tankstelle.GetAllZapfsauelen())
            {
                i++;
                Button zapfsaeuleButton = new Button()
                {
                    Content = i.ToString(),
                    Margin = new Thickness(0, 1, 0, 1)
                };
                // Set Function on Button
                zapfsaeuleButton.Click += (s, e) => SelectZapfsauele(zapfsaeule);
                ZapfsaeulenPanel.Children.Add(zapfsaeuleButton);
            }
        }

        
        private void SelectZapfsauele(Zapfsaeule zapfsaeule)
        {
            // set selection and clear children
            this.selectedZapfsaeule = zapfsaeule;
            ZapfhahnPanel.Children.Clear();

            // set new Zapfhaehne
            foreach (Zapfhahn zapfhahn in zapfsaeule.GetZapfhaene())
            {
                Button zapfhahnButton = new Button()
                {
              
                    Content = zapfhahn.GetFuelType().GetFuelTypeName(),
                    MinWidth = 50,
                    Margin = new Thickness(1)
                };
                // set Function to Button
                zapfhahnButton.Click += (s, e) => SelectZapfhahn(zapfhahn);                
                ZapfhahnPanel.Children.Add(zapfhahnButton);
            }
        }

        private void SelectZapfhahn(Zapfhahn zapfhahn)
        {
            // set selection
            this.selectedZapfhahn = zapfhahn;

            // get Information and Display
            IFuelType fuelType = this.selectedZapfhahn.GetFuelType();
            SelectedFuelLabel.Content = fuelType.GetFuelTypeName();
            CostPerLiterTextBlock.Text = $"{(decimal)fuelType.GetCostPerLiterInCent() / 100}";
            DisplayTotalFuelValue(fuelType,1);
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

        private void FuelToTakeOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        
        }


        private void TakeFuel_Click(object sender, RoutedEventArgs e)
        {
            if (selectedZapfsaeule != null && selectedZapfhahn != null)
            {
                Button tankingButton = (Button)sender;
                tankingButton.Content = "Stop";
                foreach(Button b in ZapfhahnPanel.Children)
                {
                    b.IsEnabled = false;
                    b.Background = Brushes.LightGray;
                }
                this.tankstelle.PumpGasFromZapfsauele(this.selectedZapfsaeule, this.selectedZapfhahn.GetFuelType(), DisplayTotalFuelValue);
          
            }
            else
            {
                // Errorhandling GUI
                Console.WriteLine("Keine Zapfsaeule gewaehlt");
            }
        }
    }
}
