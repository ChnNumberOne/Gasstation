using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Tankstellenkasse : Kassenautomat
    {

        // Diese liste muss angezeigt werden im GUI
        private List<Transaction> unpaidTransactions = new List<Transaction>();

        public Tankstellenkasse(List<Container> cointypes, int maximumTotalValue)
            : base(cointypes, maximumTotalValue)
        {

        }

        public void AddTransaction(Transaction transaction)
        {
            this.unpaidTransactions.Add(transaction);
        }

        // Diese Transaktionw ir mit dem Pay Button Bezahlt
        // hier muss noch mitgegeben werden was als bezahlung mitgegeben wird.
        public void PayTransaction(Transaction transaction)
        {

        }

        public List<Transaction> GetUnpaidTransactions()
        {
            return this.unpaidTransactions;
        }


    }
}
