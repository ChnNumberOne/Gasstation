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
        private Zapfsaeule currentlySelectedZapfsaeule;
        private Button currentlySelectedZapfsaeuleButton;

        public AlterItems()
        {
            InitializeComponent();
            RefreshPage();
        }

        private void RefreshPage()
        {
            DataContext = null;
            ZapfsaeulenIndex.Content = null;
            ErrorCostBoxLabel.Text = "";
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
                button.Click += (s, e) => { Zapfsaeule_Click(s, e, zapfsaeule); };
                ButtonsPanel.Children.Add(button);
            }
            SelectZapfhahn.ItemsSource = null;
            SelectFuelType.ItemsSource = GasstationState.AvailableFuelTypes;
            SelectFuelTypeCostPerL.ItemsSource = GasstationState.AvailableFuelTypes;
            SelectFuelTank.ItemsSource = null;
            SelectFuelTank.ItemsSource = GasstationState.AvailableFuelTanks;
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new WelcomePage());
        }

        private void Zapfsaeule_Click(object sender, RoutedEventArgs e, Zapfsaeule zapfsaeule)
        {
            this.DataContext = zapfsaeule;
            SelectZapfhahn.ItemsSource = zapfsaeule.Zapfhaehne;
            currentlySelectedZapfsaeule = zapfsaeule;
            Button button = (Button)sender;
            ZapfsaeulenIndex.Content = button.Content;
            currentlySelectedZapfsaeuleButton = button;
        }

        private void ZapfsaeuleAddButton_Click(object sender, RoutedEventArgs e)
        {
            Zapfsaeule zapfsaeule = Tankstelle.CreateZapfsaeule();
            /*Button button = new Button()
            {
                Content = GasstationState.AvailableZapfsaeulen.Count.ToString(),
                Margin = new Thickness(0, 1, 0, 1)
            };
            button.Click += (s, ev) => { Zapfsaeule_Click(s, e, zapfsaeule); };
            ButtonsPanel.Children.Add(button);*/
            RefreshPage();
        }
        
        private void ZapfsaeuleLoeschenButton_Click(object sender, RoutedEventArgs e)
        {
            //ButtonsPanel.Children.Remove(currentlySelectedZapfsaeuleButton);
            if (currentlySelectedZapfsaeule != null)
            {
                if (MessageBox.Show("Delete Zapfsaeule?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    GasstationState.AvailableZapfsaeulen.Remove(currentlySelectedZapfsaeule);
                    currentlySelectedZapfsaeule = null;
                    RefreshPage();
                }
            }
        }

        private void CreateZapfhahnButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectFuelType.SelectedItem != null && currentlySelectedZapfsaeule != null)
            {
                FuelType ft = (FuelType)SelectFuelType.SelectedItem;
                currentlySelectedZapfsaeule.Zapfhaehne.Add(new Zapfhahn(ft.GetFuelTypeName()));
                SelectZapfhahn.ItemsSource = null;
                SelectZapfhahn.ItemsSource = currentlySelectedZapfsaeule.Zapfhaehne;
            }
        }

        private void DeleteZapfhahnButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectZapfhahn.SelectedItem != null)
            {
                currentlySelectedZapfsaeule.Zapfhaehne.Remove((Zapfhahn)SelectZapfhahn.SelectedItem);
                SelectZapfhahn.ItemsSource = null;
                SelectZapfhahn.ItemsSource = currentlySelectedZapfsaeule.Zapfhaehne;
            }
        }

        private void SelectFuelTypeCostPerL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentCostPerLiter.Content = (((FuelType)SelectFuelTypeCostPerL.SelectedItem).CostPerLiterInCent / 100m).ToString("C2");
        }

        private void SaveCostPerLiter_Click(object sender, RoutedEventArgs e)
        {
            int costInCent;
            if (int.TryParse(CostPerLiterBox.Text, out costInCent) && SelectFuelTypeCostPerL.SelectedItem != null)
            {
                ((FuelType)SelectFuelTypeCostPerL.SelectedItem).CostPerLiterInCent = costInCent;
                CurrentCostPerLiter.Content = "";
                CurrentCostPerLiter.Content = (((FuelType)SelectFuelTypeCostPerL.SelectedItem).CostPerLiterInCent / 100m).ToString("C2");
            }
        }

        private void CostPerLiterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ignoreInt;
            if (!int.TryParse(CostPerLiterBox.Text, out ignoreInt) && !string.IsNullOrEmpty(CostPerLiterBox.Text))
            {
                ErrorCostBoxLabel.Text = "Input not valid";
            }
            else
            {
                ErrorCostBoxLabel.Text = "";
            }
        }
    }
}
