using Gasstation.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstellenkasse
    {
        private readonly Kassenautomat legacyAutomat;

        private readonly IDataRepository dataRepository;

        private readonly List<Transaction> paidTransactions = new List<Transaction>();

        private List<Transaction> unpaidTransactions = new List<Transaction>(); // nein
                        
        public Tankstellenkasse(
            IDataRepository dataRepository,
            List<Container> cointypes, 
            int maximumTotalValue)
        {
            legacyAutomat = new Kassenautomat(cointypes, maximumTotalValue);

            this.dataRepository = dataRepository;
            this.paidTransactions = LoadPreviousTransaction(dataRepository);
        }


        /// <summary>
        /// Adds a Transaction to the Unpaid Transactions List
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(Transaction transaction)
        {
            this.unpaidTransactions.Add(transaction);
        }

        /// <summary>
        /// Makes the Paying Process on the TankstellenKasse
        /// Get the Change for the Transaction
        /// Save the Transactions new Values
        /// Move the Transaction to the Paid List
        /// </summary>
        /// <param name="transaction"></param>
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
        /// <returns></returns>
        public List<Transaction> GetUnpaidTransactions()
        {
            return this.unpaidTransactions;
        }

        private void SaveTransaction()
        {
            this.dataRepository.StoredTransactions = this.paidTransactions;
        }

        private static List<Transaction> LoadPreviousTransaction(IDataRepository dataRepository)
        {
            return dataRepository.StoredTransactions.ToList();
        }
    }
}
