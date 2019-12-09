using System;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstelle
    {

        // für Benjamin : 
        // 

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

        public List<Zapfsaeule> AvailableZapfsaeulen = new List<Zapfsaeule>();

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
                .Select(x => {
                    IEnumerable<Zapfhahn> zapfhaehneFuerSaeule = this.AvailableFuelTypes.Select(fuelType => new Zapfhahn(fuelType));
                    return new Zapfsaeule(zapfhaehneFuerSaeule.ToList());
                });
            this.AvailableZapfsaeulen.AddRange(zapfsauelen);
        }

      
        // Zapfhaehne readonly zurückgeben
        public List<Zapfsaeule> GetAllZapfsauelen()
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
