using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_OOP_Testing.Klasses;
using WPF_OOP_Testing.Models;
using WPF_OOP_Testing.Viewmodels;

namespace WPF_OOP_Testing.Fenster
{
    /// <summary>
    /// Interaktionslogik für AlterItemsPage.xaml
    /// </summary>
    public partial class AlterItemsPage : Window
    {
        //Stores the id of the currently selected tankstelle object
        private int currentlySelected = -1;
        //list of all tankstellen
        private ObservableCollection<Tankstelle> tankstellen = SavedValues.tankstellen;
        //Model for data context
        public static TankstelleTankZapfseuleModel model = new TankstelleTankZapfseuleModel();
        public AlterItemsPage()
        {
            InitializeComponent();
            LoadAllButtons();
            model.TankViewmodel = new TankViewmodel();
            LastColumn.Content = model.TankViewmodel;
            //createSampleButtons(20);
        }
        /*
        private void createSampleButtons(int amnt)
        {
            for (int i = 1; i <= amnt; i++)
            {
                Button button = new Button()
                {
                    Content = "Button Nr. " + i.ToString(),
                    Name = "Button" + i.ToString(),
                    Margin = new Thickness(0, 1, 0, 1)
                };
                ButtonsPanel.Children.Add(button);
            }
        }*/
        //loads all existing buttons into the window
        private void LoadAllButtons()
        {
            foreach (Tankstelle tankstelle in tankstellen)
            {
                Button button = new Button()
                {
                    Content = tankstelle.Name,
                    Name = "t" + SavedValues.TankstellenID.ToString(),
                    Margin = new Thickness(0, 1, 0, 1)
                };
                button.Click += new RoutedEventHandler(TankstelleButton_Click);
                ButtonsPanel.Children.Add(button);
            }
        }

        //function for adding a tankstelle object and a button
        private void StelleAddenButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TankstellenNameBox.Text))
            {
                Tankstelle tstl = new Tankstelle(TankstellenNameBox.Text);
                tankstellen.Add(tstl);
                int id = SavedValues.tankstellen.Count;
                Button button = new Button()
                {
                    Content = TankstellenNameBox.Text,
                    Name = "t" + tstl.id,
                    Margin = new Thickness(0, 1, 0, 1)
                };
                button.Click += new RoutedEventHandler(TankstelleButton_Click);
                ButtonsPanel.Children.Add(button);
            }
            TankstellenNameBox.Text = "";
            
        }

        //on click of newly created button
        private void TankstelleButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            //CurrentTnk.Content = "Tankstelle: "+ b.Content;
            string idStr = b.Name.Substring(1);
            Tankstelle current = Tankstelle.FindTankstelle(Convert.ToInt32(idStr));
            model.Tankstelle = current;
            DataContext = model;
            currentlySelected = current.id;
        }

        private void StelleLoeschenButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentlySelected != -1)
            {
                Tankstelle toRemove = Tankstelle.FindTankstelle(currentlySelected);
                SavedValues.tankstellen.Remove(toRemove);
                string buttonName = "t" + toRemove.id.ToString();
                var child = ButtonsPanel.Children.OfType<Control>().Where(x => x.Name == buttonName).First();
                ButtonsPanel.Children.Remove(child);
                DataContext = model;
                model.Tankstelle = null;
            }
        }

        private void FuelOverview_Click(object sender, RoutedEventArgs e)
        {
            model.TankViewmodel = new TankViewmodel();
            LastColumn.Content = model.TankViewmodel;
        }

        private void ZapfseulenOverview_Click(object sender, RoutedEventArgs e)
        {
            model.ZapfseulenViewmodel = new ZapfseulenViewmodel();
            LastColumn.Content = model.ZapfseulenViewmodel;
        }

        private void SelectZapfseule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Zapfseule zpf = SelectZapfseule.SelectedItem as Zapfseule;
            model.Zapfseule = zpf;
        }
    }
}
