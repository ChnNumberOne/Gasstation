using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    [Serializable]
    class FuelTank
    {
        // constructor
        public FuelTank(FuelType fuelType, int fuelCapacity)
        {
            this.fuelType = fuelType;
            this.fuelCapacity = fuelCapacity;
            this.fuelAmount = 0;
        }

        // fields

        // the type of fuel in the fueltank
        private FuelType fuelType;

        // the amount of fuel in the fueltank
        private int fuelAmount;

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
            if (this.fuelAmount + amount < this.fuelCapacity)
            {
                this.fuelAmount += amount;
                return amount;
            }

            int fillableAmount = this.fuelCapacity - this.fuelAmount;
            this.fuelAmount += fillableAmount;
            return fillableAmount;

        }
        /// <summary>
        /// Takes an amount from the fuel tank, if the amount is too big it takes the remainder and the drained value is returned
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
    
        public int DrainFuelFromTank(int amount)
        {
            if (this.fuelAmount - amount >= 0)
            {
                this.fuelAmount -= amount;
                return amount;
            }

            int drainableAmount = amount - this.fuelAmount;
            this.fuelAmount -= drainableAmount;
            return drainableAmount;

        }

        /// <summary>
        /// returns the fill percentage of the fueltank
        /// </summary>
        /// <returns></returns>
        public float GetFillPercentage()
        {
            return (float) this.fuelAmount / (float) this.fuelCapacity * 100;
        }


    }
}