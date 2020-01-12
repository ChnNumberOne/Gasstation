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
//      class:              FuelTank
//      classDescription:   This class represents a FuelTank of a Gasstation, it can be filled fuel can be taken from it and it knows 
//                          minor details about itself like, what fueltype is in this tank or how much of it



using System;
using System.Xml.Serialization;

namespace Gasstation.Implementation
{
    [Serializable]
    [XmlInclude(typeof(FuelType))]
    public class FuelTank
    {
        /// <summary>
        /// constructor for creation of fuelTanks when not exisiting yet
        /// </summary>
        /// <param name="fuelType">the Fueltype for which to create the tank for</param>
        /// <param name="fuelCapacity">the size of the tank in liters</param>
        public FuelTank(FuelType fuelType, int fuelCapacity)
        {
            this.FuelType = fuelType;
            this.FuelCapacity = fuelCapacity;
            this.CurrentFuelAmount = 1000;
        }
        /// <summary>
        /// Default constructor required for application settings
        /// </summary>
        public FuelTank()
        {

        }

        /// <summary>
        /// Property for the current amount of fuel ( required because of application settings)
        /// </summary>
        [XmlAttribute]
        public int CurrentFuelAmount { get; set; }

        /// <summary>
        /// Property for the top warn level ( required because of application settings)
        /// </summary>
        [XmlAttribute]
        public int TopFillWarnLevel { get; set; }

        /// <summary>
        /// Property for the bottom warn level ( required because of application settings)
        /// </summary>
        [XmlAttribute]
        public int BottomFillWarnLevel { get; set; }

        /// <summary>
        /// Property for the maximum value possible on the tank ( required because of application settings)
        /// </summary>
        [XmlAttribute]
        public int FuelCapacity { get; set; }

        /// <summary>
        /// The Fueltype which the tank holds ( required because of application settings)
        /// </summary>
        [XmlElement]
        public FuelType FuelType { get; set; }


        /// <summary>
        /// gets the Fueltype of this tank
        /// </summary>
        /// <returns>the fueltype</returns>
        public FuelType GetFuelType()
        {
            return this.FuelType;
        }


        /// <summary>
        /// Adds an amount to the fuel tank, if the fuel doesnt fit its filled to capacity and the added value is returned
        /// </summary>
        /// <param name="amount">the amount to fill into the tank</param>
        /// <returns>the amount it was able to fill</returns>
      
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
        /// <param name="amount">the amount to drain from the tank</param>
        /// <returns>the drainable remainder</returns>
    
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
        /// <returns>the current fillpercentage</returns>
        public float GetFillPercentage()
        {
            return (float) this.CurrentFuelAmount / (float) this.FuelCapacity * 100;
        }

        /// <summary>
        /// gets the current amount of fuel in the tank as a fixed value
        /// </summary>
        /// <returns>the amount left in the tank</returns>
        public int GetCurrentFuelAmount()
        {
            return this.CurrentFuelAmount;
        }

        /// <summary>
        /// returns the maximum capacity allowed in the fuelTank
        /// </summary>
        /// <returns>the value which represents the maximum amount of liters it can hold</returns>
        public int GetMaxCapacity()
        {
            return this.FuelCapacity;
        }
    }
}