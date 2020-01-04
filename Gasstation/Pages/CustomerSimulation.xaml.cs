using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Gasstation.Implementation;

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für CustomerSimulation.xaml
    /// </summary>
    public partial class CustomerSimulation : Page
    {

        public static WrapPanel AccessZapfhahnPanel;

        private Zapfhahn selectedZapfhahn;

        private Zapfsaeule selectedZapfsaeule;

        private Tankstelle tankstelle;

        public CustomerSimulation()
        {
            
            InitializeComponent();
            RefreshPage();
            AccessZapfhahnPanel = ZapfhahnPanel;
            CustomerUIFrame.Content = new DisplayWelcome("Chose your gas pump", "");

        }

        private void RefreshPage()
        {
            // Singleton Initalisieren
            this.tankstelle = Tankstelle.Current();


            // Clear GUI
            ZapfsaeulenPanel.Children.Clear();
            ZapfhahnPanel.Children.Clear();
            
            // Load Again
            foreach (Zapfsaeule zapfsaeule in tankstelle.GetAllZapfsauelen())
            {
                Button zapfsaeuleButton = new Button()
                {
                    Content = zapfsaeule.GetName(),
                    Margin = new Thickness(0, 1, 0, 1)
                };
                // Set Function on Button
                zapfsaeuleButton.Click += (s, e) => SelectZapfsauele(zapfsaeule);
                ZapfsaeulenPanel.Children.Add(zapfsaeuleButton);
            }
        }

        
        public void SelectZapfsauele(Zapfsaeule zapfsaeule)
        {

            // überprüfen ob der Zapfhhan bereits läuft oder nicht. Wenn ja Schnellanzeige

            // set selection and clear children
            this.selectedZapfsaeule = zapfsaeule;
            ZapfhahnPanel.Children.Clear();

            bool isLocked = true;
            if (!zapfsaeule.isLocked())
            {
                isLocked = false;
                CustomerUIFrame.Content = new DisplayWelcome("Chose your gas nozzle", "", new Uri("../Images/Zapfhahn.png", UriKind.Relative));
            }
            // set new Zapfhaehne
            foreach (Zapfhahn zapfhahn in zapfsaeule.GetZapfhaene())
            {
                Button zapfhahnButton = new Button()
                {

                    Content = zapfhahn.GetFuelType().GetFuelTypeName(),
                    MinWidth = 50,
                    Margin = new Thickness(1),
                    IsEnabled = !isLocked
                };
                if (isLocked)
                {
                    zapfhahnButton.Background = Brushes.LightGray;
                }
                // set Function to Button
                zapfhahnButton.Click += (s, e) => SelectZapfhahn(zapfhahn);
                ZapfhahnPanel.Children.Add(zapfhahnButton);
            }
            if (isLocked && selectedZapfsaeule.GetSelectedZapfhahn() != null)
            {
                SelectZapfhahn(selectedZapfsaeule.GetSelectedZapfhahn());
            }
        }

        public void SelectZapfhahn(Zapfhahn zapfhahn)
        {
            // set selection
            this.selectedZapfhahn = zapfhahn;
            selectedZapfsaeule.Selectzapfhahn(zapfhahn);
            CustomerUI customerUI = new CustomerUI();
            CustomerUIFrame.Content = customerUI;
            customerUI.SetZapfhahnValues(selectedZapfsaeule, zapfhahn, this);

        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

    }
}
