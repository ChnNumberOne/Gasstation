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

        public KassenUI(Transaction transaction)
        {
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
        }
    }
}
