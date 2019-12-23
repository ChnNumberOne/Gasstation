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

        public static WrapPanel AccessZapfhahnPanel;

        private Zapfhahn selectedZapfhahn;

        private Zapfsaeule selectedZapfsaeule;

        private Tankstelle tankstelle;

        public CustomerSimulation()
        {
            
            InitializeComponent();
            RefreshPage();
            AccessZapfhahnPanel = ZapfhahnPanel;
            CustomerUIFrame.Content = new DisplayWelcome("Chose your gas pump", "");

        }

        /*
        private void DisplayTotalFuelValue(IFuelType fuelType, int currentFuelTransaction, Zapfsaeule runningZapfsaeule)
        {
            if(runningZapfsaeule == selectedZapfsaeule)
            {

                // TODO: BENJAMIN
                // Das hier updated nun sauber aber wenn wir die Zapfsaeule wechseln nicht instant sondern erst beim nächsten TimerInterval / Elapsed
                // überleg dir obs besser wäre das direkt zu updaten beim change via Button oder ob wir einfach die Requenz vom TImer erhöhen sollen
                // ACHTUNG TIMER FREQUENZ ERHÖHEN bedeutet schnelleres Tanken was wir entgenewirken müssen
                // BENJAMIN:
                // Wir möchtens Ja nur per Knopfdruck ändern, per TimerInterval wäre unsauber
                CostBox.Text = currentFuelTransaction * (decimal)fuelType.GetCostPerLiterInCent() / 100 + ".-";
            }
            
        }*/

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

            //TODO: BENJAMIN
            // Das hier kümmert sich um die Anzeige der Transaktionen auf der Kasse.
            // Das wird aber nur refreshed wenn wir ins menu zurückgehen. obviously. Versuch das hier sauber anzuzeigen
            // würd mir n haufen stress abnehmen -> der foreach unten für die anzeige der Kasse
            /*
            foreach(Transaction t in tankstelle.GetTransactionList())
            {
                // Anzeige der Transaktion
                // hier kann dann acuh die funktion mit ne closure auf den click oder so gelegt werden um eine Selektion fürs pay zu machen?
                TextBlock printTextBlock = new TextBlock();
                printTextBlock.Text = t.GetTotalFuelAmount().ToString();
                //QuittungenPanel.Children.Add(printTextBlock);
            }*/
        }

        
        private void SelectZapfsauele(Zapfsaeule zapfsaeule)
        {
            // set selection and clear children
            this.selectedZapfsaeule = zapfsaeule;
            ZapfhahnPanel.Children.Clear();

            bool isLocked = true;
            if (!zapfsaeule.isLocked())
            {
                isLocked = false;
                CustomerUIFrame.Content = new DisplayWelcome("Chose your gas nozzle", "", new Uri("../Images/Zapfhahn.png", UriKind.Relative));
            }
            // set new Zapfhaehne
            foreach (Zapfhahn zapfhahn in zapfsaeule.GetZapfhaene())
            {
                Button zapfhahnButton = new Button()
                {

                    Content = zapfhahn.GetFuelType().GetFuelTypeName(),
                    MinWidth = 50,
                    Margin = new Thickness(1),
                    IsEnabled = !isLocked
                };
                if (isLocked)
                {
                    zapfhahnButton.Background = Brushes.LightGray;
                }
                // set Function to Button
                zapfhahnButton.Click += (s, e) => SelectZapfhahn(zapfhahn);
                ZapfhahnPanel.Children.Add(zapfhahnButton);
            }
            if (isLocked && selectedZapfsaeule.GetSelectedZapfhahn() != null)
            {
                SelectZapfhahn(selectedZapfsaeule.GetSelectedZapfhahn());
            }
        }

        private void SelectZapfhahn(Zapfhahn zapfhahn)
        {
            // set selection
            this.selectedZapfhahn = zapfhahn;
            selectedZapfsaeule.Selectzapfhahn(zapfhahn);

            // get Information and Display
            CustomerUI customerUI = new CustomerUI();
            //IFuelType fuelType = this.selectedZapfhahn.GetFuelType();
            CustomerUIFrame.Content = customerUI;
            customerUI.SetZapfhahnValues(selectedZapfsaeule, zapfhahn);
            //SelectedFuelLabel.Content = fuelType.GetFuelTypeName();
            //CostPerLiterTextBlock.Text = $"{(decimal)fuelType.GetCostPerLiterInCent() / 100}.-";
            // TODO: BENJAMIN
            // das hier sollte das zwar updatene von oben aber vielleicht hab ich hier was falsch überlegt.
            //DisplayTotalFuelValue(fuelType,1, selectedZapfsaeule);
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }
        /*
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
        // TODO: BENJAMIN
        // Brauch hier ASAP aufm GUI was zum selektieren der Noten zum bezahlen
        // Prio 1 nach Bugfixes sonst kann ich nicht weitermachen.
        private void PayBetrag_Click(object sender, RoutedEventArgs e)
        {
            // ka isch crap
            //this.tankstelle.PayBill(this.selectedZapfsaeule);
        }*/
    }
}
