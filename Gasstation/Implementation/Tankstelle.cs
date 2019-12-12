using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstelle
    {

        // für Benjamin : 

        // 1.

        // Observable Collection. geht im gleichen Zug zu 2.

        // 2.

        // Die Business Logik soll im grunde genommen die GUI Logik ergänzen
        // Heisst: was in der Buisnesslogik passiert muss ja jetzt auch angezeigt werden.
        // Buttons disablen für die Fueltypes wenn einer ausgewählt wurde auf der Zapfsaeule
        // Nutzer wählt Diesel auf Säule 1
        // Nutzer wählt Starten
        // Nutzer wählt Benzin auf Säule 1 <- soll das GUI nicht erlauben Button.disable


        // 3. Feld Amount of Fuel to Take out -> remove


        // ODERMATT WIRD TESTEN:
        // GRUNDFUNKTIONEN
        // ZWEITE SEITE:
        // KLASSENDIAGRAMM SEQUENZDIAGRAMM PROGRAMM STIMMEN ÜBEREIN
        // FEHLERKONTROLLE UMSETZEN
        // DATENPERSISTENZ UND EXCEPTIONHANDLING MÜSSEN UMGESETZT WERDEN
        // DOKUMENTATION MUSS SO VERFASST SEIN, DASS ODERMATT DRAUSKOMMT

        // ObservableCollection


        // Singleton
        private static Tankstelle currentInstance;

        public static Tankstelle Current()
        {
            return currentInstance ?? (currentInstance = new Tankstelle());
        }

        public ObservableCollection<Zapfsaeule> AvailableZapfsaeulen = new ObservableCollection<Zapfsaeule>();

        public List<FuelTank> AvailableFuelTanks = new List<FuelTank>();

        public List<FuelType> AvailableFuelTypes = new List<FuelType>();

        // Konstruktor mit Basiswerten Initialisierung
        private Tankstelle()
        {
            // Erstellen der FuelTypes und zuweisen auf State
            this.AvailableFuelTypes = new List<FuelType>
            {
                new Benzin(),
                new Diesel(),
                new Bleifrei(),
            };

            // Für jeden Fueltype einen FuelTank erstellen 
            IEnumerable<FuelTank> fuelTanks = this.AvailableFuelTypes.Select(fuelType => new FuelTank(fuelType, 1000));
            this.AvailableFuelTanks.AddRange(fuelTanks);

            // Liste von Zapfsaeulen erstellen
            // für jede Zapfsaeule   ->
            // für jeden FuelType einen Zapfhahn erstellen
            IEnumerable<Zapfsaeule> zapfsauelen =
                Enumerable
                .Range(0, 5)
                .Select(x =>
                {
                    IEnumerable<Zapfhahn> zapfhaehneFuerSaeule = this.AvailableFuelTypes.Select(fuelType => new Zapfhahn(fuelType));
                    return new Zapfsaeule(zapfhaehneFuerSaeule.ToList());
                });

            foreach(Zapfsaeule e in zapfsauelen)
            {
                this.AvailableZapfsaeulen.Add(e);
            }
        }

      
        // Zapfhaehne readonly zurückgeben
        public IList<Zapfsaeule> GetAllZapfsauelen()
        {

            return this.AvailableZapfsaeulen;
        }

        // Sprit Bezug
        public void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType, decimal amount)
        {

            // Zapfsaeule sperren fals nicht gesperrt und Tankprozess starten
            if (zapfsaeule.isTanking())
            {
                zapfsaeule.StopTankingTimer();
            }
            else
            {
                if (!zapfsaeule.isLocked())
                {
                    zapfsaeule.Lock();

                    Console.WriteLine(fuelType);

                    FuelTank currentFuelTank = this.AvailableFuelTanks.Find(x => x.GetFuelType() == fuelType);

                    zapfsaeule.StartTankingTimer(currentFuelTank);

                }
            }
         

            // Timer abbrechen über Zapfsaeule.StopTankingTimer()
        }
    }
}
