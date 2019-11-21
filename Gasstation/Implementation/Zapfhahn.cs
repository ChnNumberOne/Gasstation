using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Zapfhahn : IZapfhahn
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

        public Zapfhahn(string fuelTypeName)
        {
            this.fuelTank = GasstationState.AvailableFuelTanks.Find(tank => tank.GetFuelType().GetFuelTypeName() == fuelTypeName);
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

    public interface IZapfhahn
    {
        FuelType GetFuleType();

        bool IsLocked();

        void Drain(int amount);

        void Release();
    }
}
