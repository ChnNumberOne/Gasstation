using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Transaction
    {
        public Transaction(int costPerLiterInCent, int amount, FuelType fueltype)
        {
            SetDateTimeStampNow();
            this.costPerLiterInCent = costPerLiterInCent;
            this.amount = amount;
            this.fuelType = fueltype;
        }

        private int costPerLiterInCent = 0;

        private int amount = 0;

        private FuelType fuelType;

        private DateTime paymentDateTime;

        public int GetCostPerLiterInCent()
        {
            return this.costPerLiterInCent;
        }

        // Returns the cost in total money
        public float GetCostInMoney()
        {
            return (this.amount * (float)this.costPerLiterInCent / 100);
        }

        // returns the cost in cents
        public int GetCostInCent()
        {
            return this.amount * this.costPerLiterInCent;
        }

        public int GetTotalFuelAmount()
        {
            return this.amount;
        }

        public void SetDateTimeStampNow()
        {
            this.paymentDateTime = DateTime.Now;
        }
      
        public DateTime GetDateTime()
        {
            return this.paymentDateTime;
        }

        public FuelType GetFuelType()
        {
            return this.fuelType;
        }
    }
}
