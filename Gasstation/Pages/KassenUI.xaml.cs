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
using System.Windows.Shapes;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für KassenUI.xaml
    /// </summary>
    public partial class KassenUI : Window
    {
        // selected transaction
        private Transaction transaction;

        // list of inserted coin/money values
        private List<int> insertedMoney;

        // instance of customerUI for changing items
        private CustomerUI customerUI;

        // instance of gas station
        private Tankstelle tankstelle;

        // constructor
        public KassenUI(Transaction transaction, CustomerUI customerUI)
        {
            this.tankstelle = Tankstelle.Current();
            this.customerUI = customerUI;
            insertedMoney = new List<int>();

            InitializeComponent();
            this.transaction = transaction;
            Betrag.Content = transaction.GetCostInMoney().ToString("C2");
            IReadOnlyList<int> coinValues = this.tankstelle.GetAvailableCoins();
            foreach (int coinValue in coinValues)
            {
                Button button = new Button()
                {
                    Content = ((decimal)coinValue / 100).ToString("C2"),
                    FontSize = 20,
                    Background = Brushes.LightGreen,
                    Foreground = Brushes.Black,
                    Margin = new Thickness(3)
                };
                
                button.Click += (s, e) => { OnMoneyButton_Click(s, e, coinValue); };
                MoneyPanel.Children.Add(button);
            }
        }

        // on click of coin button
        private void OnMoneyButton_Click(object sender, RoutedEventArgs eventArgs, int valueInCent)
        {
            insertedMoney.Add(valueInCent);
            Button button = new Button()
            {
                Content = ((float)valueInCent / 100).ToString("C2"),
                Background = Brushes.LightGreen,
                Foreground = Brushes.Black,
                Margin = new Thickness(3)
            };
            button.Click += (s, e) => { InsertedPanel.Children.Remove((Button)s); insertedMoney.Remove(valueInCent); UpdateInserted(); };
            InsertedPanel.Children.Add(button);
            if (transaction.GetCostInCent() <= insertedMoney.Sum())
            {
                PayButton.IsEnabled = true;
                PayButton.ClearValue(BackgroundProperty);
            }
            UpdateInserted();
        }

        // update GUI
        private void UpdateInserted(bool wasPayed = false)
        {
            InsertedAmount.Content = ((float)insertedMoney.Sum() / 100).ToString("C2");
            if (transaction.GetCostInCent() <= insertedMoney.Sum() && !wasPayed)
            {
                PayButton.IsEnabled = true;
                PayButton.ClearValue(BackgroundProperty);
            }
            else
            {
                PayButton.IsEnabled = false;
                PayButton.Background = Brushes.LightGray;
            }
        }

        // closes cash register
        private void TakeRetourButton_Click(object sender, RoutedEventArgs e)
        {
            customerUI.RefreshTransactions();
            this.Close();
        }

        // on pay button click
        private void PayButton_Click(object sender, RoutedEventArgs e)
        {

            if (tankstelle.GetTransactionList().Contains(transaction))
            {
                // opens receipt GUI
                (new Receipt(transaction, insertedMoney.Sum())).Show();
                InsertedPanel.Children.Clear();
                ReturnLabel.Content = ((float)(insertedMoney.Sum() - transaction.GetCostInCent()) / 100).ToString("C2");
                PayButton.IsEnabled = false;
                PayButton.Background = Brushes.LightGray;
                TakeRetourButton.IsEnabled = true;
                TakeRetourButton.ClearValue(BackgroundProperty);
                MoneyPanel.Children.Clear();

                // list of change
                List<int> changeList = tankstelle.PayTransaction(transaction, insertedMoney);
                customerUI.ResetCustomerUI();
            

                UpdateInserted(true);
                customerUI.CostBox.Text = "0.-";
                if (changeList != null)
                {
                    // displays change on GUI
                    foreach (int coin in changeList)
                    {
                        Button button = new Button()
                        {
                            Content = ((float)coin / 100).ToString("C2"),
                            FontSize = 20,
                            Background = Brushes.Orange,
                            Foreground = Brushes.Black,
                            Margin = new Thickness(3)
                        };
                        InsertedPanel.Children.Add(button);
                    }
                }
            }
            else
            {
                this.Close();
                MessageBox.Show("Transaction has already been paid.");
            }
        }
    }
}
