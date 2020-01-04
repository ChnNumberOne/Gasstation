using System;
using System.Xml.Serialization;

namespace Gasstation.Implementation
{
    [Serializable]
    public class Transaction
    {
        private readonly Action onTransactionComplete;

        /// <summary>
        /// Used for serialization and deserialization
        /// </summary>
        public Transaction()
        {
        }

        public Transaction(int costPerLiterInCent, int amount, FuelType fueltype, string zapfsaulenName, Action onTransactionComplete)
        {
            SetDateTimeStampNow();
            this.CostPerLiterInCent = costPerLiterInCent;
            this.Amount = amount;
            this.onTransactionComplete = onTransactionComplete;
            this.FuelTypeName = fueltype.GetFuelTypeName();
            this.ZapfsauleName = zapfsaulenName;
        }

        [XmlAttribute]
        public int CostPerLiterInCent { get; set; }

        [XmlAttribute]
        public int Amount { get; set; }

        [XmlAttribute]
        public DateTime PaymentDateTime { get; set; }

        [XmlAttribute]
        public string FuelTypeName { get; set; }

        [XmlAttribute]
        public string ZapfsauleName { get;  set; }

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

        public string GetFuelTypeName()
        {
            return this.FuelTypeName;
        }

        public string GetZapfsauleName()
        {
            return this.ZapfsauleName;
        }

        public void Complete()
        {
            if (this.onTransactionComplete == null)
            {
                throw new InvalidOperationException("Transaction already completed");
            }

            this.onTransactionComplete.Invoke();
        }
    }
}
