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
//      class:              Transaction
//      classDescription:   A Transaction is what is generated when a Customer has recieved a service from the station (pumping fuel)
//                          A Transaction holds all Information relevant to the drainage of fuel; like what type how much and where, it also
//                          holds if it was paid or not and can inform about how much it has cost. This is detached from the FuelType itself




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

        /// <summary>
        /// When a new Transaction is generated in the Process
        /// </summary>
        /// <param name="costPerLiterInCent">the cost of the fueltype per liter</param>
        /// <param name="amount"></param>
        /// <param name="fueltype"></param>
        /// <param name="zapfsaulenName"></param>
        public Transaction(int costPerLiterInCent, int amount, FuelType fueltype, string zapfsaulenName) // SUPER IMPORTANT!: DO NOT USE FUELTYPE FOR COST CALCULATION DO NOT REMOVE COSTPERLITERINCENT
        {
            SetDateTimeStampNow();
            this.CostPerLiterInCent = costPerLiterInCent;
            this.Amount = amount;
            this.FuelTypeName = fueltype.GetFuelTypeName();
            this.ZapfsauleName = zapfsaulenName;
            this.IsPaid = false;
        }

        /// <summary>
        /// The cost of the liter during this time / THIS IS VITAL
        /// </summary>
        [XmlAttribute]
        public int CostPerLiterInCent { get; set; }

        /// <summary>
        /// the amount that was bought / drained
        /// </summary>
        [XmlAttribute]
        public int Amount { get; set; }

        /// <summary>
        /// The time when the transaction was created
        /// </summary>
        [XmlAttribute]
        public DateTime TransactionDateTime { get; set; }

        /// <summary>
        /// the name of the fueltype
        /// </summary>
        [XmlAttribute]
        public string FuelTypeName { get; set; }

        /// <summary>
        /// the Zapfsäule on which this was bought
        /// Carefull this is volatile/ changing zapfsaeulen on the station can make this appear in wrong context
        /// </summary>
        [XmlAttribute]
        public string ZapfsauleName { get;  set; }


        /// <summary>
        /// State if was paid or not
        /// </summary>
        [XmlAttribute]
        public bool IsPaid { get; set; }


        /// <summary>
        /// gets the cost of 1 liter of fuel during this transaction
        /// </summary>
        /// <returns></returns>
        public int GetCostPerLiterInCent()
        {
            return this.CostPerLiterInCent;
        }

        /// <summary>
        /// gets the total cost of this transaction in CHF / USD
        /// </summary>
        /// <returns>total cost of this transaction</returns>
        public float GetCostInMoney()
        {
            return (this.Amount * (float)this.CostPerLiterInCent / 100);
        }

        /// <summary>
        /// gets the total cost of this transaction IN RAPPEN / CENT
        /// </summary>
        /// <returns>total cost of this transaction</returns>
        public int GetCostInCent()
        {
            return this.Amount * this.CostPerLiterInCent;
        }

        /// <summary>
        /// gets the amount of fuel that was drained
        /// </summary>
        /// <returns>amount of duel drained</returns>
        public int GetTotalFuelAmount()
        {
            return this.Amount;
        }

        /// <summary>
        /// sets the current datetime as datetime of this transaction being done
        /// </summary>
        public void SetDateTimeStampNow()
        {
            this.TransactionDateTime = DateTime.Now;
        }
      
        /// <summary>
        ///  gets the datetime of when the transaction was done
        /// </summary>
        /// <returns>the datetime of the transactions completion</returns>
        public DateTime GetDateTime()
        {
            return this.TransactionDateTime;
        }

        /// <summary>
        /// gets the fueltype as name which was bought
        /// </summary>
        /// <returns>the name of the fueltype</returns>
        public string GetFuelTypeName()
        {
            return this.FuelTypeName;
        }

        /// <summary>
        /// gets the name of the Zapfsaeule that this transaction was made on
        /// carefull! volatile information / this might be seen in wrong context
        /// </summary>
        /// <returns>the name of the Zapfsaeule</returns>
        public string GetZapfsauleName()
        {
            return this.ZapfsauleName;
        }

        /// <summary>
        /// Checks if the Transaction was Paid
        /// </summary>
        /// <returns>state of transaction payment</returns>
        public bool WasPaid()
        {
            return this.IsPaid;
        }

        /// <summary>
        /// sets the transaction as paid
        /// </summary>
        public void SetAsPaid()
        {
            this.IsPaid = true;
        }

        /// <summary>
        /// unlocks the Zapfsaeule after completion of the transaction
        /// </summary>
        public void Complete()
        {
            Tankstelle tankstelle = Tankstelle.Current();
            foreach (Zapfsaeule zapfsaeule in tankstelle.GetAllZapfsauelen())
            {
                if (zapfsaeule.GetName() == this.ZapfsauleName)
                {
                    zapfsaeule.Unlock();
                }
            }
        }
    }
}
