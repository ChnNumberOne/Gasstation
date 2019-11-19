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
using WPF_OOP_Testing.Fenster;
using WPF_OOP_Testing.Klasses;
using WPF_OOP_Testing.Models;

namespace WPF_OOP_Testing.Views
{
    /// <summary>
    /// Interaktionslogik für ZapfseulenView.xaml
    /// </summary>
    public partial class ZapfseulenView : UserControl
    {
        private TankstelleTankZapfseuleModel model = AlterItemsPage.model;
        public ZapfseulenView()
        {
            InitializeComponent();
            DataContext = model;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (model.Tankstelle == null)
            {
                MessageBox.Show("Please select a Tankstelle first");
            }
            else if (!string.IsNullOrEmpty(SeulenName.Text) && model.Tankstelle != null)
            {
                Zapfseule zapfseule = new Zapfseule()
                {
                    Name = SeulenName.Text
                };
                model.Tankstelle.Zapfseulen.Add(zapfseule);
            }
            SeulenName.Text = "";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            model.Tankstelle.Zapfseulen.Remove(model.Zapfseule);
        }
    }
}
