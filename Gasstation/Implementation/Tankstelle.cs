using Gasstation.Interfaces;
using Gasstation.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

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

        private IDataRepository dataRepository = new DataRepository();


        private static List<FuelTank> LoadPreviousFuelTanks(IDataRepository dataRepository)
        {
            return dataRepository.StoredFuelTanks.ToList();
        }

        // Konstruktor mit Basiswerten Initialisierung
        private Tankstelle()
        {
            // Schnittstelle für Settings zum Config laden

            this.AvailableFuelTanks = LoadPreviousFuelTanks(this.dataRepository);
            if(this.AvailableFuelTanks.Any() == false)
            {
                this.AvailableFuelTypes = new List<FuelType>
                {
                    new FuelType("Benzin", 120),
                    new FuelType("Diesel", 130),
                    new FuelType("Biodiesel", 100),
                };
                foreach (FuelType ft in this.AvailableFuelTypes)
                {
                    this.AvailableFuelTanks.Add(new FuelTank(ft, 1000));
                }
            }
            else
            {
                this.AvailableFuelTypes = this.AvailableFuelTanks.Select(x => x.GetFuelType()).Distinct().ToList();
            }

            // Erstellen einer Tankstellenkasse
            List<Container> containers = LoadPreviousContainers(this.dataRepository);
            if (!containers.Any())
            {
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

                SaveContainers();
            }
            else
            {
                this.cointype = containers;
            }

            this.tankstellenkasse = new Tankstellenkasse(this.dataRepository, this.cointype, 10000);

            IEnumerable<Zapfsaeule> zapfsauelen =
                Enumerable
                .Range(1, 5)
                .Select(zapfsaeulenNummer =>
                {
                    IEnumerable<Zapfhahn> zapfhaehneFuerSaeule = this.AvailableFuelTypes.Select(fuelType => new Zapfhahn(fuelType));
                    Zapfsaeule zapfsaeule = new Zapfsaeule(zapfsaeulenNummer.ToString(), zapfhaehneFuerSaeule.ToList());
                    foreach (Transaction unpaidTransaction in this.tankstellenkasse.GetUnpaidTransactions())
                    {
                        if (unpaidTransaction.GetZapfsauleName() == zapfsaeulenNummer.ToString())
                        {
                            zapfsaeule.Lock();
                            zapfsaeule.Selectzapfhahn(zapfsaeule.GetZapfhaene().Find(x => x.GetFuelType().GetFuelTypeName() == unpaidTransaction.GetFuelTypeName()));
                        }
                    }
                    return zapfsaeule;
                });
            AvailableZapfsaeulen.AddRange(zapfsauelen);
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
                zapfsaeule.StartTankingTimer(currentFuelTank, this.SaveFuelTanks);
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
            SaveContainers();
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

        public void SaveFuelTanks()
        {
            this.dataRepository.StoredFuelTanks = this.AvailableFuelTanks;
        }

        // gets all fueltypes
        public List<FuelType> GetAvailableFuelTypes()
        {
            return this.AvailableFuelTypes;
        }

        // gets all fueltanks
        public List<FuelTank> GetAvailableFuelTanks()
        {
            return AvailableFuelTanks;
        }

        // statistic calculations

        /// <summary>
        /// calculates total profit from last year
        /// </summary>
        /// <returns></returns>        
        public float GetYearStats()
        {
            float totalMoney = 0;
            
            foreach (Transaction transaction in this.tankstellenkasse.GetPaidTransactions())
            {
                if (transaction.GetDateTime().Year == DateTime.Now.AddYears(-1).Year)
                {
                    totalMoney += transaction.GetCostInMoney();
                }
            }
            return totalMoney;
        }

        /// <summary>
        /// calculates total profit from last month
        /// </summary>
        /// <returns></returns>
        public float GetMonthStats()
        {
            float totalMoney = 0;
            string lastMonth = DateTime.Now.AddMonths(-1).ToString("yyyyMM");

            foreach (Transaction transaction in this.tankstellenkasse.GetPaidTransactions())
            {
                if (transaction.GetDateTime().ToString("yyyyMM") == lastMonth)
                {
                    totalMoney += transaction.GetCostInMoney();
                }
            }
            return totalMoney;
        }

        /// <summary>
        /// calculates total profit from last week
        /// </summary>
        /// <returns></returns>
        public float GetWeekStats()
        {
            float totalMoney = 0;
            DateTime lastWeek = DateTime.Now.AddDays(-7);

            foreach (Transaction transaction in this.tankstellenkasse.GetPaidTransactions())
            {
                if (transaction.GetDateTime() >= lastWeek && transaction.GetDateTime() < DateTime.Now.AddDays(-1))
                {
                    totalMoney += transaction.GetCostInMoney();
                }
            }
            return totalMoney;
        }

        /// <summary>
        /// calculates total profit from yesterday
        /// </summary>
        /// <returns></returns>
        public float GetTodaysMoneyStats()
        {
            float totalMoney = 0;

            foreach (Transaction transaction in this.tankstellenkasse.GetPaidTransactions())
            {
                if (transaction.GetDateTime().ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    totalMoney += transaction.GetCostInMoney();
                }
            }
            return totalMoney;
        }

        /// <summary>
        /// calculates yesterdays total liters from a certain fueltype
        /// </summary>
        /// <param name="fuelType"></param>
        /// <returns></returns>
        public int GetTodaysLiterStats(FuelType fuelType)
        {
            int totalLiters = 0;

            // gets fuel amount of paid transactions
            foreach (Transaction transaction in this.tankstellenkasse.GetPaidTransactions())
            {
                if (transaction.GetDateTime().ToShortDateString() == DateTime.Now.ToShortDateString() && transaction.GetFuelTypeName() == fuelType.GetFuelTypeName())
                {
                    totalLiters += transaction.GetTotalFuelAmount();
                }
            }

            // gets fuel amount of unpaid transactions
            foreach (Transaction transaction in this.tankstellenkasse.GetUnpaidTransactions())
            {
                if (transaction.GetDateTime().ToShortDateString() == DateTime.Now.ToShortDateString() && transaction.GetFuelTypeName() == fuelType.GetFuelTypeName())
                {
                    totalLiters += transaction.GetTotalFuelAmount();
                }
            }

            return totalLiters;
        }

        /// <summary>
        /// returns the fueltank according to the appropriate fueltype
        /// </summary>
        /// <param name="fuelType"></param>
        /// <returns></returns>
        public FuelTank FindFuelTank(string fuelType)
        {
            return AvailableFuelTanks.Find(x => x.GetFuelType().GetFuelTypeName() == fuelType);
        }

        /// <summary>
        /// Saves the containers from cointype into a file
        /// </summary>
        private void SaveContainers()
        {
            this.dataRepository.StoredContainers = cointype;
        }

        /// <summary>
        /// Loads list of containers from file
        /// </summary>
        /// <param name="dataRepository"></param>
        /// <returns></returns>
        private List<Container> LoadPreviousContainers(IDataRepository dataRepository)
        {
            return dataRepository.StoredContainers.ToList();
        }
    }
}
