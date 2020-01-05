using Gasstation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gasstation.Implementation
{
    [Serializable]
    [XmlInclude(typeof(FuelType))]
    public class FuelTank
    {
        // constructor
        public FuelTank(FuelType fuelType, int fuelCapacity)
        {
            this.FuelType = fuelType;
            this.FuelCapacity = fuelCapacity;
            this.CurrentFuelAmount = 1000;
        }

        public FuelTank()
        {

        }

        // the amount of fuel in the fueltank
        [XmlAttribute]
        public int CurrentFuelAmount { get; set; }

        // the top fill warn level of the fueltank ( e.g. do something above 70% fill)
        [XmlAttribute]
        public int TopFillWarnLevel { get; set; }

        // the bottom fill warn level of the fueltank ( e.g. do something below 30% fill)
        [XmlAttribute]
        public int BottomFillWarnLevel { get; set; }

        // the maximum capacity the tank can hold in fuel
        [XmlAttribute]
        public int FuelCapacity { get; set; }

        [XmlElement]
        public FuelType FuelType { get; set; }

        public FuelType GetFuelType()
        {
            return this.FuelType;
        }


        /// <summary>
        /// Adds an amount to the fuel tank, if the fuel doesnt fit its filled to capacity and the added value is returned
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
      
        public int AddFuelToTank(int amount)
        {
            if (this.CurrentFuelAmount + amount < this.FuelCapacity)
            {
                this.CurrentFuelAmount += amount;
                return amount;
            }

            int fillableAmount = this.FuelCapacity - this.CurrentFuelAmount;
            this.CurrentFuelAmount += fillableAmount;
            return fillableAmount;

        }
        /// <summary>
        /// Takes an amount from the fuel tank, if the amount is too big it takes the remainder and the drained value is returned
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
    
        public int DrainFuel(int amount)
        {
            Console.WriteLine(FuelType.GetFuelTypeName());
            if (this.CurrentFuelAmount - amount >= 0)
            {
                this.CurrentFuelAmount -= amount;
    
                return amount;
            }
           int drainableAmount = this.CurrentFuelAmount;

           
            this.CurrentFuelAmount -= drainableAmount;
    
            return drainableAmount;

        }

        /// <summary>
        /// returns the fill percentage of the fueltank
        /// </summary>
        /// <returns></returns>
        public float GetFillPercentage()
        {
            return (float) this.CurrentFuelAmount / (float) this.FuelCapacity * 100;
        }

  

    }
}