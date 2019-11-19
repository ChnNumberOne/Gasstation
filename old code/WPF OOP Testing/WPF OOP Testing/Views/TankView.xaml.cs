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
using WPF_OOP_Testing.Models;
using WPF_OOP_Testing.Klasses;
using WPF_OOP_Testing.Fenster;

namespace WPF_OOP_Testing.Views
{
    /// <summary>
    /// Interaktionslogik für TankView.xaml
    /// </summary>
    public partial class TankView : UserControl
    {
        private TankstelleTankZapfseuleModel model = AlterItemsPage.model;
        public TankView()
        {
            InitializeComponent();
            DataContext = model;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (model.Tankstelle == null)
                {
                    MessageBox.Show("Please select a Zapfseule first");
                }
                else if (string.IsNullOrEmpty(FuelName.Text) || string.IsNullOrEmpty(LiterAnz.Text) || string.IsNullOrEmpty(LiterAnzMax.Text))
                {
                    ErrorMessage.Content = "Please fill in all the blanks.";
                }
                else if (Convert.ToDouble(LiterAnzMax.Text) < Convert.ToDouble(LiterAnz.Text))
                {
                    MessageBox.Show("Amount of liters should not be larger than max liters.");
                    ErrorMessage.Content = "";
                }
                else
                {
                    Treibstoff treibstoff = new Treibstoff()
                    {
                        Name = FuelName.Text,
                        AnzLiter = Convert.ToDouble(LiterAnz.Text),
                        MaxCapacity = Convert.ToDouble(LiterAnzMax.Text)
                    };
                    model.Zapfseule.Treibstoffe.Add(treibstoff);
                    ErrorMessage.Content = "";
                }
            }
            catch
            {
                MessageBox.Show("Anz. & Max Anz. Liter darf keine Buchstaben enthalten");
                ErrorMessage.Content = "";
            }
            FuelName.Text = "";
            LiterAnz.Text = "";
            LiterAnzMax.Text = "";
        }

        private void FuelCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.Treibstoff = FuelCombobox.SelectedItem as Treibstoff;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
                model.Zapfseule.Treibstoffe.Remove(FuelCombobox.SelectedItem as Treibstoff);
        }
    }
}
