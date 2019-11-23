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

        private List<FuelTank> fuelTanks;

        private string fuelTypeName;


        // saves amount of total drained fuel
        private int drainedFuelTotal = 0;



        /// <summary>
        /// Erstellt einen Zapfhahn für einen STRING fueltypeName
        /// Sucht alle Tanks dieses fuelTypes heraus und bindet diese auf den zapfhahn
        /// </summary>
        /// <param name="fuelType"></param>

        public Zapfhahn(string fuelTypeName)
        {
            this.fuelTanks = GasstationState.AvailableFuelTanks.FindAll(tank => tank.GetFuelType().GetFuelTypeName() == fuelTypeName);
            this.fuelTypeName = fuelTypeName;
        }

        public string GetFuelTypeName() 
        {
            return fuelTypeName;
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

        // changes fueltank of Zapfhahn
        // TODO: need rework
        //public void ChangeFuelTank(FuelTank fuelTank)
        //{
        //    this.fuelTank = fuelTank;
        //}

        ///// <summary>
        ///// Bezieht den Benzinbetrag vom Tank
        ///// </summary>
        ///// <param name="fuel"></param>
        ///// <returns></returns>
        //public int RemoveFromFuelTank(int fuel)
        //{
        //    return this.fuelTank.DrainFuelFromTank(fuel);
        //}

     

        //// returns fueltank (For information in Configuration for GUI)
        //public FuelTank GetFuelTank()
        //{
        //    return fuelTank;
        //}
    }
}
