//
//      author:             Thomas Fischer  t.fischer@siemens.com
//      date:               11/1/2020   
//      projectname:        tankstelle / Gasstation
//      version:            1.0
//      description:        a framework for a Gasstation application. 
//                          Based on a GUI on WPF Pages
//                          Warning this is an explorative code and may have instabilities dead code or wrong design decisions
//                          
//
//      class:              Tankstelle
//      classDescription:   Main Application, leads the communication process between the Gasstation and the Frontedn GUI Application
//                          Contains all Major Runtime Information like, Which FuelTanks are available or which fueltypes.
//                          Allows Tanking from it or paying on it



using Gasstation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstelle : ITankstelle
    {
        /// <summary>
        /// current Instance of the Singleton
        /// </summary>
        private static Tankstelle currentInstance;

        /// <summary>
        /// returns / creates the Singleton
        /// </summary>
        /// <returns></returns>
        public static Tankstelle Current()
        {
            return currentInstance ?? (currentInstance = new Tankstelle());
        }

        /// <summary>
        /// List of all Zapfsaeulen Objects that are stored and available
        /// </summary>
        private List<Zapfsaeule> availableZapfsaeulen = new List<Zapfsaeule>();

        /// <summary>
        /// List of all FuelTank Objects that are stored and available
        /// </summary>
        private List<FuelTank> availableFuelTanks = new List<FuelTank>();

        /// <summary>
        /// All currently Available FuelTypes on a Zapfsaeule or on a Tank in this Station
        /// </summary>
        private List<FuelType> availableFuelTypes = new List<FuelType>();

        /// <summary>
        /// All Cointypes that exist in the Tankstellenkasse
        /// </summary>
        private List<Container> cointype = new List<Container>();

        /// <summary>
        /// The Kasse which is in Charge of Paying Transactions and the Paying Process
        /// </summary>
        private Tankstellenkasse tankstellenkasse;

        /// <summary>
        /// a Refrence to the DataRepository which allows Saving into the application settings file
        /// </summary>
        private IDataRepository dataRepository = new DataRepository();

       /// <summary>
       /// Gets the List of FuelTanks stored in the application settings via the data repository
       /// </summary>
       /// <param name="dataRepository"> the datarepository reference which holds the settings informations</param>
       /// <returns></returns>
        private static List<FuelTank> LoadPreviousFuelTanks(IDataRepository dataRepository)
        {
            return dataRepository.StoredFuelTanks.ToList();
        }

        /// <summary>
        /// Standard constructor called when the programm is initialized / started. called when there is no instance when the singleton.current() function is called
        /// </summary>
        private Tankstelle()
        {

            this.availableFuelTanks = LoadPreviousFuelTanks(this.dataRepository);
            if (this.availableFuelTanks.Any() == false)
            {
                this.availableFuelTypes = new List<FuelType>
                {
                    new FuelType("Benzin", 120),
                    new FuelType("Diesel", 130),
                    new FuelType("Biodiesel", 100),
                };
                foreach (FuelType ft in this.availableFuelTypes)
                {
                    this.availableFuelTanks.Add(new FuelTank(ft, 1000));
                }
            }
            else
            {
                this.availableFuelTypes = this.availableFuelTanks.Select(x => x.GetFuelType()).Distinct().ToList();
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
                    IEnumerable<Zapfhahn> zapfhaehneFuerSaeule = this.availableFuelTypes.Select(fuelType => new Zapfhahn(fuelType));
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
            availableZapfsaeulen.AddRange(zapfsauelen);
        }


        /// <summary>
        /// Getter for the List of Zapfsaeulen currently Available
        /// </summary>
        /// <returns>the list of all available zapfsaeulen</returns>
        public IList<Zapfsaeule> GetAllZapfsauelen()
        {
            return this.availableZapfsaeulen;
        }

        /// <summary>
        /// Switch for:
        /// Pumps Gas from a selected Zapfsäule. If the Zapfsaeule is already Tanking the Process is stopped
        /// If The Zapfsaeule isnt tanking yet the Zapfsaeule is locked and the Tanking process is started
        /// </summary>
        /// <param name="zapfsaeule">the selected zapfsaeule to tank from</param>
        /// <param name="fuelType">the selected fueltype to take from the zapfsaeule</param>
        public void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType)
        {
            if (zapfsaeule.isTanking())
            {
                this.tankstellenkasse.AddTransaction(zapfsaeule.StopTankingTimer());
            }
            else
            {
                FuelTank currentFuelTank = this.availableFuelTanks.Find(x => x.GetFuelType() == fuelType);
                zapfsaeule.StartTankingTimer(currentFuelTank, this.SaveFuelTanks);
                zapfsaeule.Lock();
            }
        }
       
        /// <summary>
        /// Takes an open Transaction and a List of Coins and Bills and Pays it via "Kassenautomat"
        /// </summary>
        /// <param name="billToPay">the Transaction that holds the information to make the payment</param>
        /// <param name="insertedMoney">a list of integer representig the coins inserted</param>
        public List<int> PayTransaction(Transaction billToPay, IList<int> insertedMoney)
        {
            List<int> output = tankstellenkasse.PayTransaction(billToPay, insertedMoney);
            SaveContainers();
            return output;
        }

        /// <summary>
        /// Gets the TransactionList of UnapidTransactions from the application settings
        /// </summary>
        /// <returns>a list of int values representig the change from the kassenautomat</returns>
        public List<Transaction> GetTransactionList()
        {
            return this.tankstellenkasse.GetUnpaidTransactions();
        }

        /// <summary>
        /// gets all available cointype sizes as an integer list
        /// </summary>
        /// <returns>a list of int values representing the value of coins available</returns>
        public IReadOnlyList<int> GetAvailableCoins()
        {
            return this.cointype.Select(x => x.GetValue()).ToList().AsReadOnly();
        }

        /// <summary>
        /// saves the list of available fuel tanks in the application setting
        /// </summary>
        public void SaveFuelTanks()
        {
            this.dataRepository.StoredFuelTanks = this.availableFuelTanks;
        }

        /// <summary>
        /// gets all the available fueltypes of the gasstation
        /// </summary>
        /// <returns>the list of all avialable fuel types</returns>
        public List<FuelType> GetAvailableFuelTypes()
        {
            return this.availableFuelTypes;
        }

        /// <summary>
        /// gets the list of all available fueltanks of the tankstelle
        /// </summary>
        /// <returns>the list of fueltanks</returns>
        public List<FuelTank> GetAvailableFuelTanks()
        {
            return availableFuelTanks;
        }

        /// <summary>
        /// calculates total profit from last year
        /// </summary>
        /// <returns>Total money from last year</returns>        
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
        /// <returns>Total money from last month</returns>
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
        /// <returns>Total money from last week</returns>
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
        /// calculates total profit from today
        /// </summary>
        /// <returns>Total money from today</returns>
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
        /// calculates todays total liters from a certain fueltype
        /// </summary>
        /// <param name="fuelType">The fuel type from which the amount of liters needs to be calculated</param>
        /// <returns>Amount of drained liters from tank according to the fuel type</returns>
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
        /// <param name="fuelType">Name of the tanks fuel type</param>
        /// <returns>Tank according to fueltype</returns>
        public FuelTank FindFuelTank(string fuelType)
        {
            return availableFuelTanks.Find(x => x.GetFuelType().GetFuelTypeName() == fuelType);
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
        /// <param name="dataRepository">Data repository of the gas station</param>
        /// <returns>List of coin/money containers</returns>
        private static List<Container> LoadPreviousContainers(IDataRepository dataRepository)
        {
            return dataRepository.StoredContainers.ToList();
        }
    }
}
