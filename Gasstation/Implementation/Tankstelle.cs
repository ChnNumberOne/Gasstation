using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstelle
    {

        // ODERMATT WIRD TESTEN:
        // GRUNDFUNKTIONEN
        // ZWEITE SEITE:
        // KLASSENDIAGRAMM SEQUENZDIAGRAMM PROGRAMM STIMMEN ÜBEREIN
        // FEHLERKONTROLLE UMSETZEN
        // DATENPERSISTENZ UND EXCEPTIONHANDLING MÜSSEN UMGESETZT WERDEN
        // DOKUMENTATION MUSS SO VERFASST SEIN, DASS ODERMATT DRAUSKOMMT


        // Singleton
        private static Tankstelle currentInstance;

        public static Tankstelle Current()
        {
            return currentInstance ?? (currentInstance = new Tankstelle());
        }

        public List<Zapfsaeule> AvailableZapfsaeulen = new List<Zapfsaeule>();

        public List<FuelTank> AvailableFuelTanks = new List<FuelTank>();

        public List<FuelType> AvailableFuelTypes = new List<FuelType>();

        public List<Transaction> OpenTransactions = new List<Transaction>();

        public List<Transaction> CompletedTransactions = new List<Transaction>();

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
            AvailableZapfsaeulen.AddRange(zapfsauelen);

        }

      
        // Zapfhaehne readonly zurückgeben
        public IList<Zapfsaeule> GetAllZapfsauelen()
        {

            return this.AvailableZapfsaeulen;
        }

        // Sprit Bezug
        public void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType, Action<FuelType,int> callback)
        {

            if (zapfsaeule.isTanking())
            {   //2
                // beenden vom Tankprozess
                Transaction finishedTransaction = zapfsaeule.StopTankingTimer();
                OpenTransactions.Add(finishedTransaction);
            }
            else
            {   //1
                // wenn nicht gesperrt starten
                if (!zapfsaeule.isLocked())
                {
                    zapfsaeule.Lock();
                    FuelTank currentFuelTank = this.AvailableFuelTanks.Find(x => x.GetFuelType() == fuelType);
                    zapfsaeule.StartTankingTimer(currentFuelTank, callback);

                }
            }
        }
    }
}
