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
//      class:              Tankstellenkasse
//      classDescription:   This is the Register System of the Gasstaion using old Legacy Code from the Kassenautomat. This uses Composition over Inheritance
//                          because a Tankstellenkasse may be the same as Kassenautomat ( nearly) but its touching old Legacy Code and with this it is easily exchangable
//                          This uses the old Kassenautomat for simpler tasks like getting change for an amount that was paid, but implements new methods like creating Transactions.


using Gasstation.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstellenkasse
    {

        /// <summary>
        /// the old Kassenautomat which should not be touched by the rest of the programm
        /// this is interchangeable
        /// </summary>
        private readonly Kassenautomat legacyAutomat;

        /// <summary>
        /// a Reference to the application settings
        /// </summary>
        private readonly IDataRepository dataRepository;

        /// <summary>
        /// the list of all transactons that have been paid already
        /// </summary>
        private readonly List<Transaction> paidTransactions = new List<Transaction>();

        /// <summary>
        /// the list of all transactions that have not been paid yet.
        /// </summary>
        private List<Transaction> unpaidTransactions = new List<Transaction>();
                        
        /// <summary>
        /// Constructor when the tankstellenkasse is initialized at the beginning
        /// </summary>
        /// <param name="dataRepository">the reference to the settings file</param>
        /// <param name="cointypes">a list of all cointypes that are available / accepted</param>
        /// <param name="maximumTotalValue">the total amount of money that the Kassenautomat can handle</param>
        public Tankstellenkasse( IDataRepository dataRepository, List<Container> cointypes, int maximumTotalValue)
        {
            legacyAutomat = new Kassenautomat(cointypes, maximumTotalValue);
            this.dataRepository = dataRepository;
            HandleTransactions(LoadPreviousTransaction(dataRepository));
         
        }

        public Transaction Transaction
        {
            get => default;
            set
            {
            }
        }

        public Kassenautomat Kassenautomat
        {
            get => default;
            set
            {
            }
        }


        /// <summary>
        /// Adds a Transaction to the Unpaid Transactions List
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(Transaction transaction)
        {
            this.unpaidTransactions.Add(transaction);
            SaveTransaction();
        }

        /// <summary>
        /// Makes the Paying Process on the TankstellenKasse
        /// Get the Change for the Transaction
        /// Save the Transactions new Values
        /// Move the Transaction to the Paid List
        /// </summary>
        /// <param name="transaction">the bill that needs to be paid</param>
        /// <param name="insertedMoney">the list of coin values inserted</param>
        public List<int> PayTransaction(Transaction transaction, IList<int> insertedMoney)
        {
            // Payment process
            foreach (int value in insertedMoney)
            {
                this.legacyAutomat.InsertCoin(value);
            }

            List<int> changeCoins = null;


            if (transaction.GetCostInCent() <= this.legacyAutomat.GetValueInput())
            {
                int changeValue = this.legacyAutomat.GetValueInput() - transaction.GetCostInCent();
                changeCoins = this.legacyAutomat.GetChange(changeValue);
                transaction.SetDateTimeStampNow();

                transaction.SetAsPaid();
                this.paidTransactions.Add(transaction);
                this.unpaidTransactions.Remove(transaction);
            }

            this.legacyAutomat.ResetAutomat();

            transaction.Complete();
            SaveTransaction();
            

            return changeCoins;
        }

        /// <summary>
        /// Gets the Unpaid Transactions List
        /// </summary>
        /// <returns>The list of all unpaid transaction</returns>
        public List<Transaction> GetUnpaidTransactions()
        {
            return this.unpaidTransactions;
        }

        /// <summary>
        /// gets the list of all paid transactions
        /// </summary>
        /// <returns>the list of all paid transactions</returns>
        public List<Transaction> GetPaidTransactions()
        {
            return this.paidTransactions;
        }

        /// <summary>
        /// Saves all Transactions in the Application settings
        /// </summary>
        private void SaveTransaction()
        {
            List<Transaction> allTransactions = new List<Transaction>(this.paidTransactions);
            allTransactions.AddRange(this.unpaidTransactions);
            this.dataRepository.StoredTransactions = allTransactions;
        }

        /// <summary>
        /// Loads the Previously saved Transactions from the application settings
        /// </summary>
        /// <param name="dataRepository">the refrence to the application settings</param>
        /// <returns>the list of all stored transactions</returns>
        private static List<Transaction> LoadPreviousTransaction(IDataRepository dataRepository)
        {
            return dataRepository.StoredTransactions.ToList();
        }


        private void HandleTransactions(List<Transaction> transactions)
        {
            foreach (Transaction transaction in transactions)
            {
                if (transaction.WasPaid())
                {
                    paidTransactions.Add(transaction);
                }
                else
                {
                    unpaidTransactions.Add(transaction);
                }
            }
        }
    }
}
