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
using Gasstation.Testfolder;
using Gasstation.Pages;

namespace Gasstation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MainFrame;

        public MainWindow()
        {

            InitializeComponent();
            MainMenuControl.Content = new WelcomePage();
            MainFrame = MainMenuControl;
            //ApplicationSetup();
            AppSetup();
            


        }


        public void AppSetup() 
        {
            // Erstellen der FuelTypes und zuweisen auf State
            GasstationState.AvailableFuelTypes = new List<FuelType>
            {
                new Benzin(),
                new Diesel(),
                new Bleifrei(),

            };

            // für jeden FuelType ein Tank erstellen und zuweisen auf State
            GasstationState.AvailableFuelTanks = new List<FuelTank>();
            foreach(FuelType fuelType in GasstationState.AvailableFuelTypes)
            {
                GasstationState.AvailableFuelTanks.Add(new FuelTank(fuelType, 1000));
            }

            // 5 Zapfsaeulen generieren
            for(int i = 0; i < 5; i++)
            {
                List<Zapfhahn> zapfhaehneFuerSaeule = new List<Zapfhahn>();
                foreach (FuelType fuelType in GasstationState.AvailableFuelTypes)
                {
                    zapfhaehneFuerSaeule.Add(new Zapfhahn(fuelType));
                }

                Zapfsaeule zapfsaeule = new Zapfsaeule(zapfhaehneFuerSaeule);
                GasstationState.AvailableZapfsaeulen.Add(zapfsaeule);
            }
           
        }

        public static void SetContent(object newPage)
        {
            MainFrame.Content = newPage;
        }
        /*
        public void ApplicationSetup()
        {
            // === Beispiel wie  eine Zapfsaeule erstellt wird. ===
            // preconditions
            // erstelle Benzin Sorten und Benzin Tanks zuerst
            FuelType ft1 = new FuelType("Benzin", 120);
            FuelType ft2 = new FuelType("Diesel", 130);

            // füge diese dem State hinzu
            GasstationState.AvailableFuelTypes.Add(ft1);
            GasstationState.AvailableFuelTypes.Add(ft2);

            FuelTank fuelt1 = new FuelTank(ft1, 1000);
            FuelTank fuelt2 = new FuelTank(ft2, 1000);

            GasstationState.AvailableFuelTanks.Add(fuelt1);
            GasstationState.AvailableFuelTanks.Add(fuelt2);

            // Tankstelle besitzt einen State in dem Benzintypen und BenzinTanks vorhanden sint


        }

        int counter = 0;

        // Button Click Event 
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // erstelle alle Zapfhaehne
            Zapfsaeule zs = Tankstelle.CreateZapfsaeule();
            GasstationState.AvailableZapfsaeulen.Add(zs);
            Console.WriteLine("#### AUSGABE GESPEICHERTER FUELTYPES ####");
            foreach (FuelTank ft in GasstationState.AvailableFuelTanks)
            {
                Console.WriteLine(ft.GetFuelType().GetFuelTypeName());
                Console.WriteLine(ft.GetFuelType().GetCostPerLiterInCent());
            }
            Console.WriteLine("#### AUSGABE ZAPFSAEULE ####");
            Console.WriteLine(zs.ToString());












            // Wie sollte ein Bezug aussehen Example:

            // vom GUI kommt ein Identifier werlchen Sprit wir beziehen und von wo
            String desiredFuelTypeName = "Benzin";
            Zapfsaeule selectedZapfsaeule = Tankstelle.CustomerSelectZapfsauele(0);
            // wir überprüfen ob alle Zaehne dieser Zapfsaeule freigegeben sind
            


            // wir hohlen für den Identifiere das FuelType objekt
            FuelType desiredFuelType = GasstationState.AvailableFuelTypes.Find(x => x.GetFuelTypeName().Equals(desiredFuelTypeName));

            // wir selektieren den Zapfhahn dessen FuelType der gleiche ist wie den wir suchen.
            Zapfhahn selectedZapfhahn = selectedZapfsaeule.GetZapfhahnOfFuelType(desiredFuelType);

            // locki g unlocking
       
                //tanking;













            //// Test für Konfigurationsschreiben (muss nicht hier sein)
            //FuelType testFuelType = new FuelType("Benzin", 125);
            //FuelType testFuelType2 = new FuelType("Diesel", 135);
            //FuelType testFuelType3 = new FuelType("Super", 130);
            //List<FuelType> testFuelTypes = new List<FuelType>();
            //testFuelTypes.Add(testFuelType);
            //testFuelTypes.Add(testFuelType2);
            //testFuelTypes.Add(testFuelType3);





            //FuelTank newCreatedFuelTank = new FuelTank(new FuelType("Benzin", 125), 1000);
            //Button eventButton = (Button)e.Source;
            //StackPanel stackPanel = (StackPanel)eventButton.Parent;

            //Button newTankButton = new Button();
            //newTankButton.Content = "Tank " + (counter++);
            //newTankButton.Click += (s, events) =>
            //{
            //    newCreatedFuelTank.AddFuelToTank(100);
            //    Console.WriteLine(newCreatedFuelTank.GetFillPercentage());
            //    newCreatedFuelTank.DrainFuelFromTank(50);
            //    Console.WriteLine(newCreatedFuelTank.GetFillPercentage());

            //};
            //stackPanel.Children.Add(newTankButton);

            //// grober test für config save ( auf diese weise können wir bereits Objekte serialisiern und abspeichern und wieder laden.
            //// alles was fehlt ist Datenerweiterung der Konfiguration und absicherung, dass der IFormatter richtig aufgebaut ist.
            //List<FuelTank> fuelTanks = new List<FuelTank>();
            //fuelTanks.Add(newCreatedFuelTank);
            //Config config = Config.CreateInstance();
            //config.SetConfigurationData(fuelTanks);
            //config.SaveConfig();
            //config.ReadConfig();



            //// test für Zapfsaeule
            //Zapfsaeule zapfS = new Zapfsaeule(fuelTanks);

            //// kann nicht getestet werden wegen fehler in Methode StartTanking
            //zapfS.StartTanking(120);


            //zapfS.SelectZapfhahn(testFuelType);
            //zapfS.StartTanking(120);
            //zapfS.UnlockAllZapfhaehne();
            //zapfS.StartTanking(120);
        }
         private void AlterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckStatsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CustomerViewButton_Click(object sender, RoutedEventArgs e)
        {

        }
        */
    }
}
