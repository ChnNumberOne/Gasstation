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
        private Transaction transaction;
        private static List<KassenUI> kassenUIs = new List<KassenUI>();
        private List<int> insertedMoney;
        private CustomerUI CustomerUI;

        public KassenUI(Transaction transaction, CustomerUI customerUI)
        {
            this.CustomerUI = customerUI;
            insertedMoney = new List<int>();
            foreach (KassenUI kassenUI in kassenUIs)
            {
                kassenUI.Close();
                //kassenUIs.Remove(kassenUI);
            }
            kassenUIs.Clear();
            kassenUIs.Add(this);
            int no;
            no = kassenUIs.Count;
            InitializeComponent();
            this.transaction = transaction;
            Betrag.Content = transaction.GetCostInMoney().ToString("C2");
            int[] nums = { 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000 };
            foreach (int i in nums)
            {
                Button button = new Button()
                {
                    Content = ((float)i / 100).ToString("C2"),
                    FontSize = 20,
                    Background = Brushes.LightGreen,
                    Foreground = Brushes.Black,
                    Margin = new Thickness(3)
                };
                button.Click += (s, e) => { OnMoneyButton_Click(s, e, i); };
                MoneyPanel.Children.Add(button);
            }
        }

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

        private void TakeRetourButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerUI.RefreshTransactions();
            this.Close();
        }

        // AN THOMAS
        // Bitte hier beim pay das ganze von der kasse auch integrieren. Jetzt grad ist nur ein bisschen makeshift code eingebaut.
        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            InsertedPanel.Children.Clear();
            ReturnLabel.Content = ((float)(insertedMoney.Sum() - transaction.GetCostInCent()) / 100).ToString("C2");
            PayButton.IsEnabled = false;
            PayButton.Background = Brushes.LightGray;
            TakeRetourButton.IsEnabled = true;
            TakeRetourButton.ClearValue(BackgroundProperty);
            MoneyPanel.Children.Clear();
            // Das ist wirklich nur so demonstrativer code. Das muss später geändert werden.
            Tankstelle.Current().tankstellenkasse.GetUnpaidTransactions().Remove(transaction);
            UpdateInserted(true);
        }
    }
}
