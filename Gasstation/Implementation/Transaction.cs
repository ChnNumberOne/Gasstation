using System;
using System.Xml.Serialization;

namespace Gasstation.Implementation
{
    [Serializable]
    public class Transaction
    {
        /// <summary>
        /// Used for serialization and deserialization
        /// </summary>
        public Transaction()
        {
        }

        public Transaction(int costPerLiterInCent, int amount, FuelType fueltype, Zapfsaeule createdOnZapfsaeule)
        {
            SetDateTimeStampNow();
            this.CostPerLiterInCent = costPerLiterInCent;
            this.Amount = amount;
            this.fuelType = fueltype;
            this.createdOnZapfsaeule = createdOnZapfsaeule;
        }

        private Zapfsaeule createdOnZapfsaeule;
        private FuelType fuelType;

        [XmlAttribute]
        public int CostPerLiterInCent { get; set; }

        [XmlAttribute]
        public int Amount { get; set; }

        [XmlAttribute]
        public DateTime PaymentDateTime { get; set; }

        public int GetCostPerLiterInCent()
        {
            return this.CostPerLiterInCent;
        }

        // Returns the cost in total money
        public float GetCostInMoney()
        {
            return (this.Amount * (float)this.CostPerLiterInCent / 100);
        }

        // returns the cost in cents
        public int GetCostInCent()
        {
            return this.Amount * this.CostPerLiterInCent;
        }

        public int GetTotalFuelAmount()
        {
            return this.Amount;
        }

        public void SetDateTimeStampNow()
        {
            this.PaymentDateTime = DateTime.Now;
        }
      
        public DateTime GetDateTime()
        {
            return this.PaymentDateTime;
        }

        // TODO TOX: Replace with FueltypeName
        public FuelType GetFuelType()
        {
            return this.fuelType;
        }

        public Zapfsaeule GetCreatedOnZapfsaeule()
        {
            return this.createdOnZapfsaeule;
        }
    }
}
