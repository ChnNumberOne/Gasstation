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
        // static wrappanel for changing items from other pages
        public static WrapPanel AccessZapfhahnPanel;

        // instance of selected gas pump
        private Zapfsaeule selectedZapfsaeule;

        // instance of gas station
        private Tankstelle tankstelle;

        // constructor
        public CustomerSimulation()
        {
            InitializeComponent();
            RefreshPage();
            AccessZapfhahnPanel = ZapfhahnPanel;
            CustomerUIFrame.Content = new DisplayWelcome("Chose your gas pump", "");

        }

        // updates the page
        private void RefreshPage()
        {
            // Initializes singleton
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

        // on selection of gas pump
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

        // on selection of gas nozzle
        public void SelectZapfhahn(Zapfhahn zapfhahn)
        {
            selectedZapfsaeule.Selectzapfhahn(zapfhahn);
            CustomerUI customerUI = new CustomerUI();
            CustomerUIFrame.Content = customerUI;
            customerUI.SetZapfhahnValues(selectedZapfsaeule, zapfhahn, this);

        }

        // returns to main menu
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

    }
}
