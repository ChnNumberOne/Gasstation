using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Tankstellenkasse : Kassenautomat
    {

     
        private List<Transaction> unpaidTransactions = new List<Transaction>();
        private List<Transaction> paidTransactions = new List<Transaction>();

        public Tankstellenkasse(List<Container> cointypes, int maximumTotalValue)
            : base(cointypes, maximumTotalValue)
        {

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
            foreach(int value in insertedMoney)
            {
                this.InsertCoin(value);
            }

            List<int> changeCoins = null;
            if ( transaction.GetCostInCent() <= this.GetValueInput())
            {
                int changeValue = this.GetValueInput() - transaction.GetCostInCent();
                changeCoins = this.GetChange(changeValue);
                transaction.SetDateTimeStampNow();
                

                this.paidTransactions.Add(transaction);
                this.unpaidTransactions.Remove(transaction);
            }
            // returns Null if there isnt any change
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

     




    }
}
