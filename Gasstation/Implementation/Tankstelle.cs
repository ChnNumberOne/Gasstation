using Gasstation.Interfaces;
using Gasstation.Properties;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstelle
    {


        // Singleton
        private static Tankstelle currentInstance;

        public static Tankstelle Current()
        {
            return currentInstance ?? (currentInstance = new Tankstelle());
        }

        public List<Zapfsaeule> AvailableZapfsaeulen = new List<Zapfsaeule>(); // nei

        public List<FuelTank> AvailableFuelTanks = new List<FuelTank>(); // ja

        public List<FuelType> AvailableFuelTypes = new List<FuelType>(); // nei

        private List<Container> cointype = new List<Container>(); // nei ( tox anschauen)

        private Tankstellenkasse tankstellenkasse; // das ( das hed viel shit dinne)

       
          

      

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
                .Select(zapfsaulenNummer =>
                {
                    IEnumerable<Zapfhahn> zapfhaehneFuerSaeule = this.AvailableFuelTypes.Select(fuelType => new Zapfhahn(fuelType));
                    return new Zapfsaeule(zapfsaulenNummer.ToString(), zapfhaehneFuerSaeule.ToList());
                });
            AvailableZapfsaeulen.AddRange(zapfsauelen);


            // Erstellen einer Tankstellenkasse
            this.cointype.Add(new Container(10, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(20, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(50, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(100, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(200, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(500, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(1000, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(2000, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(5000, 0, 1000, 100, 900, 100));
            this.cointype.Add(new Container(10000, 0, 1000, 100, 900, 100));




            IDataRepository dataRepository = new DataRepository();
            this.tankstellenkasse = new Tankstellenkasse(dataRepository, this.cointype, 10000);
        }

      
        // Zapfhaehne readonly zurückgeben
        public IList<Zapfsaeule> GetAllZapfsauelen()
        {
            return this.AvailableZapfsaeulen;
        }

        // Sprit Bezug
        public void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType)
        {
            if (zapfsaeule.isTanking())
            {  
                this.tankstellenkasse.AddTransaction(zapfsaeule.StopTankingTimer());
            }
            else 
            {
                FuelTank currentFuelTank = this.AvailableFuelTanks.Find(x => x.GetFuelType() == fuelType);
                zapfsaeule.StartTankingTimer(currentFuelTank);
                zapfsaeule.Lock();
            }          
        }

        /// <summary>
        /// Takes an open Transaction and a List of Coins and Bills and Pays it via "Kassenautomat"
        /// </summary>
        /// <param name="billToPay"></param>
        /// <param name="insertedMoney"></param>
        public List<int> PayTransaction(Transaction billToPay, IList<int> insertedMoney)
        {
            List<int> output = tankstellenkasse.PayTransaction(billToPay, insertedMoney);
            return output;
        }

        public List<Transaction> GetTransactionList()
        {
            return this.tankstellenkasse.GetUnpaidTransactions();
        }

        public IReadOnlyList<int> GetAvailableCoins()
        {
            return this.cointype.Select(x => x.GetValue()).ToList().AsReadOnly();
        }
    }
}
