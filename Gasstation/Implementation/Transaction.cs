using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Transaction
    {

        // ID?

        // Datum

        // Saeule

        // Typ => name      // nicht den FuelType verwende, da dieser einen wechselndne Zustand hat

        // Preis Pro Liter

        // Menge


        public Transaction(int costPerLiterInCent, int amount)
        {
            this.costPerLiterInCent = costPerLiterInCent;
            this.amount = amount;
        }

        private int costPerLiterInCent = 0;

        private int amount = 0;

    }
}
