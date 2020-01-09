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

        private void SaveFuelTanks()
        {
            this.dataRepository.StoredFuelTanks = this.AvailableFuelTanks;
        }

        // statistic calculations

        // calculates total profit from last year
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

        // calculates total profit from last month
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

        // calculates total profit from last week
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

        // calculates total profit from yesterday
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

        // calculates total liters from yesterday
        public int GetTodaysLiterStats()
        {
            int totalLiters = 0;

            // gets fuel amount of paid transactions
            foreach (Transaction transaction in this.tankstellenkasse.GetPaidTransactions())
            {
                if (transaction.GetDateTime().ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    totalLiters += transaction.GetTotalFuelAmount();
                }
            }

            // gets fuel amount of unpaid transactions
            foreach (Transaction transaction in this.tankstellenkasse.GetUnpaidTransactions())
            {
                if (transaction.GetDateTime().ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    totalLiters += transaction.GetTotalFuelAmount();
                }
            }

            return totalLiters;
        }
    }
}
