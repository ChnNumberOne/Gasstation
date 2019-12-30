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
using System.Windows.Threading;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für CustomerUI.xaml
    /// </summary>
    public partial class CustomerUI : Page
    {
        private Zapfsaeule selectedZapfsaeule;

        private IFuelType selectedFuelType;      // für Berechnung Anzeige

        private Zapfhahn selectedZapfhahn;

        private Transaction selectedTransaction;

        private Tankstelle tankstelle;

        private DispatcherTimer guiRefreshTimer;

        private CustomerSimulation customerSimulation;

        public CustomerUI()
        {
            InitializeComponent();
            this.tankstelle = Tankstelle.Current();

            // TImer zum Anzeigen des Standes
            this.guiRefreshTimer = new DispatcherTimer();
            guiRefreshTimer.Tick += (s, e) =>
            {
                RefreshCurrentZapfsaeule();
            };
            this.guiRefreshTimer.Interval = TimeSpan.FromMilliseconds(100);
            this.guiRefreshTimer.Start();
        }

        public void SetZapfhahnValues(Zapfsaeule selectedZapfsaeule, Zapfhahn selectedZapfhahn, CustomerSimulation customerSimulation)
        {
            this.customerSimulation = customerSimulation;
            this.selectedZapfsaeule = selectedZapfsaeule;
            this.selectedZapfhahn = selectedZapfhahn;
            this.selectedFuelType = selectedZapfhahn.GetFuelType();
            // Transaction vo de Zapfsüle wo im GUI slektiert isch
            SelectedFuelLabel.Content = selectedZapfhahn.GetFuelType().GetFuelTypeName();
            CostPerLiterTextBlock.Text = $"{(decimal)selectedZapfhahn.GetFuelType().GetCostPerLiterInCent() / 100}.-";
            CostBox.Text = selectedZapfsaeule.GetCurrentTransactionFuelAmount() * (decimal)selectedZapfhahn.GetFuelType().GetCostPerLiterInCent() / 100 + ".-";
            if (selectedZapfsaeule.isTanking())
            {
                TakeFuel.Content = "Stop";
            }
            else if (selectedZapfsaeule.isLocked())
            {
                TakeFuel.Content = "Go pay";
                TakeFuel.Background = Brushes.LightGray;
                TakeFuel.IsEnabled = false;
            }
            else if (selectedZapfsaeule.isLocked() == false)
            {
                TakeFuel.Content = "Start Tanking";
                TakeFuel.ClearValue(BackgroundProperty);
                TakeFuel.IsEnabled = true;
            }
            RefreshTransactions();
        }

        private Button tankingButton;

        private void TakeFuel_Click(object sender, RoutedEventArgs e)
        {
            if (selectedZapfsaeule != null && selectedZapfhahn != null)
            {
                this.tankingButton = (Button)sender;
                if (tankingButton.Content.ToString() != "Stop")
                {

                    tankingButton.Content = "Stop";
                    foreach (Button b in CustomerSimulation.AccessZapfhahnPanel.Children)
                    {
                        b.IsEnabled = false;
                        b.Background = Brushes.LightGray;
                    }
                    this.tankstelle.PumpGasFromZapfsauele(this.selectedZapfsaeule, this.selectedZapfhahn.GetFuelType());
                }
                else
                {
                    if (selectedZapfsaeule.GetCurrentTransactionFuelAmount() > 0)
                    {
                        tankingButton.Content = "Go pay";
                        tankingButton.Background = Brushes.LightGray;
                        tankingButton.IsEnabled = false;

                        this.tankstelle.PumpGasFromZapfsauele(this.selectedZapfsaeule, this.selectedZapfhahn.GetFuelType());
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

        private void PayBetrag_Click(object sender, RoutedEventArgs e)
        {
            // ka isch crap
            //this.tankstelle.PayBill(this.selectedZapfsaeule);
            KassenUI kassenUI = new KassenUI(selectedTransaction, this);
            kassenUI.Show();

            PayBetrag.IsEnabled = false;
            PayBetrag.Background = Brushes.LightGray;

        }

        public void RefreshTransactions()
        {
            QuittungenPanel.Children.Clear();
            foreach (Transaction transaction in tankstelle.GetTransactionList())
            {
                if (transaction.GetCreatedOnZapfsaeule() == selectedZapfsaeule)
                {
                    Button button = new Button()
                    {
                        Content = transaction.GetCostInMoney().ToString("C2"),
                        Margin = new Thickness(2)
                    };
                    button.Click += (s, e) => { TransactionButton_Click(s, e, transaction); };
                    QuittungenPanel.Children.Add(button);
                }
            }
            BetragBlock.Text = "";
        }

        private void TransactionButton_Click(object sender, RoutedEventArgs e, Transaction transaction)
        {
            BetragBlock.Text = (transaction.GetTotalFuelAmount() * (float)transaction.GetCostPerLiterInCent() / 100).ToString("C2");
            selectedTransaction = transaction;
            PayBetrag.IsEnabled = true;
            PayBetrag.ClearValue(BackgroundProperty);
        }

        private void RefreshCurrentZapfsaeule()
        {
            int fuelAmountToDisplay = this.selectedZapfsaeule.GetCurrentTransactionFuelAmount();
            decimal result = fuelAmountToDisplay * (decimal)selectedFuelType.GetCostPerLiterInCent() / 100;
            CostBox.Text = result.ToString() + ".-";
        }

        public void ResetCustomerUI()
        {

            // HACK ( PLEASE REWORK )

            // TODO BENJI: Schau Bitte, dass die GUI Elemente hier richtig zurückgesetzt werden. DIes hat folgende tücken wie ich bemerkt habe:
            // 1. Wenn es nicht richtig gemacht wird, sind die buttons nicht mehr mouseover responsiv
            // 2. Es gibt eine Problematik, wenn du während des Zahlvorgangs im Hintergrund die Zapfsaeule wechselst mit der art und weise wie das GUI dargestellt ist.
            // Da sind richtig harte Bugs drinn und wir können uns drauf verlassen, dass er die findet. Es wird nichts richtig geupdatet so.
            // Das ganze Window im Hintergrund zu sperren nützt nichts, da theoretisch man nachdem man auf stop drückt auf eine andere säule gehen kann und da bezahlen kann.
            // Da musst du dir was schlaues überlegen
            // Der Hack hier unten war für mich, damit ich mein Backend noch machen konnte und sehen konnte ob sonst alles geht.
            customerSimulation.SelectZapfsauele(selectedZapfsaeule);
            customerSimulation.SelectZapfhahn(selectedZapfhahn);

            //SetZapfhahnValues(this.selectedZapfsaeule, this.selectedZapfhahn, this.customerSimulation);
            /*foreach (Button b in CustomerSimulation.AccessZapfhahnPanel.Children)
            {
                b.IsEnabled = true;
               
                b.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#373737"));
            }*/
        }
    
    }
}
