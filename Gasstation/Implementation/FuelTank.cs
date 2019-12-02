using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    [Serializable]
    public class FuelTank
    {
        // constructor
        public FuelTank(FuelType fuelType, int fuelCapacity)
        {
            this.fuelType = fuelType;
            this.fuelCapacity = fuelCapacity;
            this.currentFuelAmount = 1000;
        }

        // fields

        // the type of fuel in the fueltank
        private FuelType fuelType;

        // the amount of fuel in the fueltank
        private int currentFuelAmount;

        // the top fill warn level of the fueltank ( e.g. do something above 70% fill)
        private int topFillWarnLevel;

        // the bottom fill warn level of the fueltank ( e.g. do something below 30% fill)
        private int bottomFillWarnLevel;

        // the maximum capacity the tank can hold in fuel
        private int fuelCapacity;

        public FuelType GetFuelType()
        {
            return this.fuelType;
        }

        public void SetTopFillWarn(int newTopWarnLevel)
        {
            this.topFillWarnLevel = newTopWarnLevel;
        }

        public void SetBottomFillWarn(int newBottomWarnLevel)
        {
            this.bottomFillWarnLevel = newBottomWarnLevel;
        }

        /// <summary>
        /// Adds an amount to the fuel tank, if the fuel doesnt fit its filled to capacity and the added value is returned
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
      
        public int AddFuelToTank(int amount)
        {
            if (this.currentFuelAmount + amount < this.fuelCapacity)
            {
                this.currentFuelAmount += amount;
                return amount;
            }

            int fillableAmount = this.fuelCapacity - this.currentFuelAmount;
            this.currentFuelAmount += fillableAmount;
            return fillableAmount;

        }
        /// <summary>
        /// Takes an amount from the fuel tank, if the amount is too big it takes the remainder and the drained value is returned
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
    
        public int DrainFuel(int amount)
        {
            if (this.currentFuelAmount - amount >= 0)
            {
                this.currentFuelAmount -= amount;
                return amount;
            }
           int drainableAmount = this.currentFuelAmount;

           
            this.currentFuelAmount -= drainableAmount;
            return drainableAmount;

        }

        /// <summary>
        /// returns the fill percentage of the fueltank
        /// </summary>
        /// <returns></returns>
        public float GetFillPercentage()
        {
            return (float) this.currentFuelAmount / (float) this.fuelCapacity * 100;
        }


    }
}