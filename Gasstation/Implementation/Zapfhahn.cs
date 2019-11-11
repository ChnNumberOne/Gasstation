using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    class Zapfhahn
    {
        // bool for seeing if zapfhahn can be accessed
        private bool isLocked = false;

        // fuel tank that gets accessed by this zapfhahn
        private FuelTank fuelTank;

        // saves amount of total drained fuel
        private int drainedFuelTotal = 0;


        // constructor that uses an already created fueltank
        public Zapfhahn(FuelTank fuelTank)
        {
            this.fuelTank = fuelTank;
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

        // changes fueltank of Zapfhahn
        public void ChangeFuelTank(FuelTank fuelTank)
        {
            this.fuelTank = fuelTank;
        }

        /// <summary>
        /// Bezieht den Benzinbetrag vom Tank
        /// </summary>
        /// <param name="fuel"></param>
        /// <returns></returns>
        public int RemoveFromFuelTank(int fuel)
        {
            return this.fuelTank.DrainFuelFromTank(fuel);
        }

     

        // returns fueltank (For information in Configuration for GUI)
        public FuelTank GetFuelTank()
        {
            return fuelTank;
        }
    }
}
