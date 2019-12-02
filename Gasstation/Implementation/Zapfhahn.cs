using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Zapfhahn
    {
        // Important Notes and TOOD:
        // each Zapfhahn has an entire list of Tanks ( Because its easier)



        private bool isLocked = false;

        private FuelTank fuelTank;



        // saves amount of total drained fuel
        private int drainedFuelTotal = 0;



        /// <summary>
        /// Erstellt einen Zapfhahn für einen STRING fueltypeName
        /// Sucht den Tank dieses fuelTypes heraus und bindet diese auf den zapfhahn
        /// </summary>
        /// <param name="fuelType"></param>

        public Zapfhahn(FuelType fuelType)
        {
            this.fuelTank = GasstationState.AvailableFuelTanks.Find(tank => tank.GetFuelType().GetFuelTypeName() == fuelType.GetFuelTypeName());
        }

       

        public FuelTank GetFuelTank()
        {
            return this.fuelTank;
        }

       




        /// <summary>
        /// Boolean Check if this is locked
        /// </summary>
        /// <returns></returns>
        public bool IsLocked()
        {
            if (this.isLocked)
            {
                return true;
            }
            return false;
        }





        /// <summary>
        /// Locks this Zapfhahn
        /// </summary>
        public void LockZapfhahn()
        {
            this.isLocked = true;
        }

        /// <summary>
        /// Unlocks this Zapfhahn
        /// </summary>
        public void UnlockZapfhahn()
        {
            this.isLocked = false;
        }

        public int DrainFuelFromTank(int fuelToDrain)
        {
            
            return this.fuelTank.DrainFuel(fuelToDrain);


        }


    }

   
}
