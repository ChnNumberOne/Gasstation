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

        public List<Container> Cointypes = new List<Container>();

        public Tankstellenkasse tankstellenkasse;


      

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


            // Erstellen einer Tankstellenkasse
            this.Cointypes.Add(new Container(10, 0, 1000, 100, 900, 100));
            this.Cointypes.Add(new Container(20, 0, 1000, 100, 900, 100));
            this.Cointypes.Add(new Container(50, 0, 1000, 100, 900, 100));
            this.Cointypes.Add(new Container(100, 0, 1000, 100, 900, 100));
            this.Cointypes.Add(new Container(200, 0, 1000, 100, 900, 100));
            this.Cointypes.Add(new Container(500, 0, 1000, 100, 900, 100));
            this.tankstellenkasse = new Tankstellenkasse(this.Cointypes, 10000);
        }

      
        // Zapfhaehne readonly zurückgeben
        public IList<Zapfsaeule> GetAllZapfsauelen()
        {

            return this.AvailableZapfsaeulen;
        }

        // Sprit Bezug
        public void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType, Action<FuelType,int,Zapfsaeule> callback)
        {

            if (zapfsaeule.isTanking())
            {   //2
                // beenden vom Tankprozess
                // Beendete Transaktion speichern
                this.tankstellenkasse.AddTransaction(zapfsaeule.StopTankingTimer());
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

        // TODO: BENJAMIN
        public void PayBill()
        {
            // Das hier brauche ich Dringend wenn du das GUI was du erstellst für die Selektion der Transaktion aus der Liste verbesert hast von oben kannst du hier noch
            // schauen, dass du eine aus dem stackpanel selektieren kannst und das hier als beispiel benutzt dafür. Das hier ist der Button mit dem wir bezahlen und ich brauch die Selektion der Transaktion dort.
            // Vielleicht hast du ne Idee für das evtl brauchts dafür ne Binding List / Observable Collection. Probier mal was aus
         

        }

        public List<Transaction> GetTransactionList()
        {
            return this.tankstellenkasse.GetUnpaidTransactions();
        }
    }
}
