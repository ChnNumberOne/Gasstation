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

        private Transaction selectedTransaction;

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
            CostBox.Text = selectedZapfsaeule.GetCurrentFuelTransaction().ToString() + ".-";

            RefreshTransactions();
        }

        private void TakeFuel_Click(object sender, RoutedEventArgs e)
        {
            if (selectedZapfsaeule != null && selectedZapfhahn != null)
            {
                Button tankingButton = (Button)sender;
                if (tankingButton.Content.ToString() != "Stop")
                {

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
                    if (selectedZapfsaeule.GetCurrentFuelTransaction() > 0)
                    {
                        tankingButton.Content = "Go pay";
                        tankingButton.Background = Brushes.LightGray;
                        tankingButton.IsEnabled = false;
                        selectedZapfsaeule.StopTankingTimer();
                        RefreshTransactions();
                    }
                }
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
            KassenUI kassenUI = new KassenUI(selectedTransaction, this);
            kassenUI.Show();

            PayBetrag.IsEnabled = false;
            PayBetrag.Background = Brushes.LightGray;
        }


        // AN: THOMAS
        // Hier ist mal so ein basic refresh der die buttons erstellt. Kannst unten den button click even ändern wenn du willst
        public void RefreshTransactions()
        {
            QuittungenPanel.Children.Clear();
            foreach (Transaction transaction in tankstelle.GetTransactionList())
            {
                Button button = new Button()
                {
                    Content = transaction.GetCostInMoney().ToString("C2"),
                    Margin = new Thickness(2)
                };
                button.Click += (s, e) => { TransactionButton_Click(s, e, transaction); };
                QuittungenPanel.Children.Add(button);
            }
            BetragBlock.Text = "";
        }

        // AN: THOMAS
        //Hier ist der dynamic button click event
        private void TransactionButton_Click(object sender, RoutedEventArgs e, Transaction transaction)
        {
            BetragBlock.Text = (transaction.GetTotalFuelAmount() * (float)transaction.GetCostPerLiterInCent() / 100).ToString("C2");
            selectedTransaction = transaction;
            PayBetrag.IsEnabled = true;
            PayBetrag.ClearValue(BackgroundProperty);
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
