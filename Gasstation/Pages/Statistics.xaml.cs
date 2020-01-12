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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        // instance of gas station
        private Tankstelle tankstelle;

        // constructor
        public Statistics()
        {
            InitializeComponent();
            RefreshAllStats();
        }

        // refreshes GUI
        // sets proper values to GUI items
        private void RefreshAllStats()
        {
            this.tankstelle = Tankstelle.Current();
            TotalLastYearLabel.Content = tankstelle.GetYearStats().ToString("C2");
            TotalLastMonthLabel.Content = tankstelle.GetMonthStats().ToString("C2");
            TotalLastWeekLabel.Content = tankstelle.GetWeekStats().ToString("C2");
            TotalTodayLabel.Content = tankstelle.GetTodaysMoneyStats().ToString("C2");

            foreach (FuelType fuelType in tankstelle.GetAvailableFuelTypes())
            {
                TextBlock textBlock = new TextBlock()
                {
                    Text = fuelType.GetFuelTypeName() + ": " + tankstelle.GetTodaysLiterStats(fuelType) + "L\t"
                };
                FuelTypePanel.Children.Add(textBlock);
            }
        }
    }
}
