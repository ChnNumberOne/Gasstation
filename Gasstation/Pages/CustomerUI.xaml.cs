﻿using Gasstation.Implementation;
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
        // selected gas pump
        private Zapfsaeule selectedZapfsaeule;

        // fuel type of selected gas nozzle
        private IFuelType selectedFuelType;

        // selected gas nozzle
        private Zapfhahn selectedZapfhahn;

        // selected transaction from cash register
        private Transaction selectedTransaction;

        // instance of gas station
        private Tankstelle tankstelle;

        // gui dispatcher timer
        private DispatcherTimer guiRefreshTimer;

        // instance of previous page
        private CustomerSimulation customerSimulation;

        // instance of tanking button
        private Button tankingButton;

        // constructor
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

        // for setting GUI properly
        public void SetZapfhahnValues(Zapfsaeule selectedZapfsaeule, Zapfhahn selectedZapfhahn, CustomerSimulation customerSimulation)
        {
            this.customerSimulation = customerSimulation;
            this.selectedZapfsaeule = selectedZapfsaeule;
            this.selectedZapfhahn = selectedZapfhahn;
            this.selectedFuelType = selectedZapfhahn.GetFuelType();
            // Transaction vo de Zapfsüle wo im GUI slektiert isch
            SelectedFuelLabel.Content = selectedZapfhahn.GetFuelType().GetFuelTypeName();
            CostPerLiterTextBlock.Text = $"{(decimal)selectedZapfhahn.GetFuelType().GetCostPerLiterInCent() / 100}.-";
            CostBox.Text = this.selectedZapfsaeule.GetCurrentTransactionFuelAmount() * (decimal)this.selectedZapfhahn.GetFuelType().GetCostPerLiterInCent() / 100 + ".-";
            LiterBox.Text = this.selectedZapfsaeule.GetCurrentTransactionFuelAmount() + " L";
            if (this.selectedZapfsaeule.isTanking())
            {
                TakeFuel.Content = "Stop";
            }
            else if (this.selectedZapfsaeule.isLocked())
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

        // on TakeFuel button click
        private void TakeFuel_Click(object sender, RoutedEventArgs e)
        {
            if (selectedZapfsaeule != null && selectedZapfhahn != null)
            {
                this.tankingButton = (Button)sender;
                if (tankstelle.FindFuelTank(selectedZapfhahn.GetFuelType().GetFuelTypeName()).GetCurrentFuelAmount() > 0 || selectedZapfsaeule.isTanking())
                {
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
                        tankingButton.Content = "Go pay";
                        tankingButton.Background = Brushes.LightGray;
                        tankingButton.IsEnabled = false;

                        this.tankstelle.PumpGasFromZapfsauele(this.selectedZapfsaeule, this.selectedZapfhahn.GetFuelType());
                        RefreshTransactions();
                    }
                }
                else
                {
                    this.tankingButton.Content = "Fuel tank empty";
                    this.tankingButton.IsEnabled = false;
                    this.tankingButton.Background = Brushes.IndianRed;
                }
            }
            else
            {
                // Errorhandling GUI
                Console.WriteLine("Keine Zapfsaeule gewaehlt");
            }
        }

        // for opening cash register GUI after PayButton click
        private void PayBetrag_Click(object sender, RoutedEventArgs e)
        {
            KassenUI kassenUI = new KassenUI(selectedTransaction, this);
            kassenUI.Show();

            PayBetrag.IsEnabled = false;
            PayBetrag.Background = Brushes.LightGray;

        }

        // refreshing list of unpaid transactions
        public void RefreshTransactions()
        {
            QuittungenPanel.Children.Clear();
            foreach (Transaction transaction in tankstelle.GetTransactionList())
            {
                Button button = new Button()
                {
                    Content = "Säule " + transaction.GetZapfsauleName() + ": " + transaction.GetTotalFuelAmount().ToString() + "L, " + transaction.GetCostInMoney().ToString("C2"),
                    Margin = new Thickness(2)
                };
                button.Click += (s, e) => { TransactionButton_Click(s, e, transaction); };
                QuittungenPanel.Children.Add(button);
            }
            BetragBlock.Text = "";
        }

        // on selection of transaction
        private void TransactionButton_Click(object sender, RoutedEventArgs e, Transaction transaction)
        {
            BetragBlock.Text = (transaction.GetTotalFuelAmount() * (float)transaction.GetCostPerLiterInCent() / 100).ToString("C2");
            selectedTransaction = transaction;
            PayBetrag.IsEnabled = true;
            PayBetrag.ClearValue(BackgroundProperty);
        }

        // for refreshing GUI
        private void RefreshCurrentZapfsaeule()
        {
            int fuelAmountToDisplay = this.selectedZapfsaeule.GetCurrentTransactionFuelAmount();
            decimal result = fuelAmountToDisplay * (decimal)selectedFuelType.GetCostPerLiterInCent() / 100;
            CostBox.Text = result.ToString() + ".-";
            LiterBox.Text = this.selectedZapfsaeule.GetCurrentTransactionFuelAmount() + " L";
        }

        // reselects items from CustomerSimulation
        public void ResetCustomerUI()
        {
            customerSimulation.SelectZapfsauele(selectedZapfsaeule);
            customerSimulation.SelectZapfhahn(selectedZapfhahn);
        }
    
    }
}
