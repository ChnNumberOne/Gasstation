﻿using System;
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
            Tankstelle.Current().tankstellenkasse.AddTransaction(this);
        }

        private int costPerLiterInCent = 0;

        private int amount = 0;

        public int GetCostPerLiterInCent()
        {
            return this.costPerLiterInCent;
        }

        public float GetCostInMoney()
        {
            return (this.GetTotalFuelAmount() * (float)this.GetCostPerLiterInCent() / 100);
        }

        public int GetTotalFuelAmount()
        {
            return this.amount;
        }
            

    }
}
