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
    /// Interaktionslogik für Receipt.xaml
    /// </summary>
    public partial class Receipt : Window
    {
        public Receipt(Transaction transaction, int PaidInCent)
        {
            InitializeComponent();
            DateTimeBlock.Text = transaction.GetDateTime().ToString("MM/dd/yyyy HH:mm");
            FuelTypeBlock.Text = transaction.GetFuelType().GetFuelTypeName();
            AmntOfLitersBlock.Text = transaction.GetTotalFuelAmount().ToString() + "L";
            CostBlock.Text = transaction.GetCostInMoney().ToString("C2");
            PaidBlock.Text = ((float)PaidInCent / 100).ToString("C2");
            ChangeBlock.Text = ((float)(PaidInCent - transaction.GetCostInCent()) / 100).ToString("C2");
        }
    }
}
