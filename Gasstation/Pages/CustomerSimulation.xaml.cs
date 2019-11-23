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
using Gasstation.Implementation;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für CustomerSimulation.xaml
    /// </summary>
    public partial class CustomerSimulation : Page
    {

        public CustomerSimulation()
        {
            InitializeComponent();
            int i = 0;
            foreach (Zapfsaeule zapfsaeule in StaticConfig.Zapfsaeulen)
            {
                i++;
                Button button = new Button()
                {
                    Content = zapfsaeule.Name,
                    Name = zapfsaeule.Name.Replace(" ", "") + i,
                    Margin = new Thickness(0, 1, 0, 1)
                };
                button.Click += (s, e) => 
                {
                    Zapfsaeule zapfsaeule1 = zapfsaeule;
                    Button b = (Button)s;
                    //ZapfhahnPanel.Children.Remove();
                };
                ZapfsaeulenPanel.Children.Add(button);
            }
        }

        private void AddZapfhahnButton()
        {

        }

        private void ZapfsaeuleButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
        }

            private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

        private void FuelToTakeOut_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
