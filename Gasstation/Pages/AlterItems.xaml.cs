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
    /// Interaktionslogik für AlterItems.xaml
    /// </summary>
    public partial class AlterItems : Page
    {
        public AlterItems()
        {
            InitializeComponent();
            RefreshPage();
        }

        private void RefreshPage()
        {
            ButtonsPanel.Children.Clear();

            int i = 0;
            foreach (Zapfsaeule zapfsaeule in GasstationState.AvailableZapfsaeulen)
            {
                i++;
                Button button = new Button()
                {
                    Content = i.ToString(),
                    Margin = new Thickness(0, 1, 0, 1)
                };
                button.Click += (s, e) =>
                {
                    this.DataContext = zapfsaeule;
                };
                ButtonsPanel.Children.Add(button);
            }
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

        private void FuelOverview_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ZapfseulenOverview_Click(object sender, RoutedEventArgs e)
        {

        }


        private void SelectZapfseule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ZapfsaeuleAddButton_Click(object sender, RoutedEventArgs e)
        {
            Zapfsaeule zapfsaeule = Tankstelle.CreateZapfsaeule();
            Button button = new Button()
            {
                Content = GasstationState.AvailableZapfsaeulen.Count + 1,
                Margin = new Thickness(0, 1, 0, 1)
            };
            button.Click += (s, ev) =>
            {
                this.DataContext = zapfsaeule;
            };
            ButtonsPanel.Children.Add(button);
        }

        private void ZapfsaeuleLoeschenButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
