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
    /// Interaktionslogik für CustomerUI.xaml
    /// </summary>
    public partial class CustomerUI : Page
    {
        private Zapfhahn selectedZapfhahn;

        private Zapfsaeule selectedZapfsaeule;

        private Tankstelle tankstelle;

        public CustomerUI()
        {
            InitializeComponent();
            this.tankstelle = Tankstelle.Current();
        }

        public void SetZapfhahnValues(Zapfsaeule selectedZapfsaeule, Zapfhahn selectedZapfhahn)
        {
            this.selectedZapfsaeule = selectedZapfsaeule;
            this.selectedZapfhahn = selectedZapfhahn;

            SelectedFuelLabel.Content = selectedZapfhahn.GetFuelType().GetFuelTypeName();
            CostPerLiterTextBlock.Text = $"{(decimal)selectedZapfhahn.GetFuelType().GetCostPerLiterInCent() / 100}.-";

            foreach (Transaction t in tankstelle.GetTransactionList())
            {
                // Anzeige der Transaktion
                // hier kann dann acuh die funktion mit ne closure auf den click oder so gelegt werden um eine Selektion fürs pay zu machen?
                TextBlock printTextBlock = new TextBlock();
                printTextBlock.Text = t.GetTotalFuelAmount().ToString();
                QuittungenPanel.Children.Add(printTextBlock);
            }
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
                foreach (Button b in CustomerSimulation.AccessZapfhahnPanel.Children)
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
        }


        private void DisplayTotalFuelValue(IFuelType fuelType, int currentFuelTransaction, Zapfsaeule runningZapfsaeule)
        {
            if (runningZapfsaeule == selectedZapfsaeule)
            {

                // TODO: BENJAMIN
                // Das hier updated nun sauber aber wenn wir die Zapfsaeule wechseln nicht instant sondern erst beim nächsten TimerInterval / Elapsed
                // überleg dir obs besser wäre das direkt zu updaten beim change via Button oder ob wir einfach die Requenz vom TImer erhöhen sollen
                // ACHTUNG TIMER FREQUENZ ERHÖHEN bedeutet schnelleres Tanken was wir entgenewirken müssen
                // BENJAMIN:
                // Wir möchtens Ja nur per Knopfdruck ändern, per TimerInterval wäre unsauber
                CostBox.Text = currentFuelTransaction * (decimal)fuelType.GetCostPerLiterInCent() / 100 + ".-";
            }

        }
    }
}
