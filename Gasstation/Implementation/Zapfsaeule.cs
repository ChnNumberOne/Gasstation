using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Zapfsaeule
    {

        public Zapfsaeule(List<Zapfhahn> zapfhaehne)
        {
            this.zapfhaehne = zapfhaehne;
          
        }

       

        private List<Zapfhahn> zapfhaehne;

        private Zapfhahn currentlySelectedZapfhahn;


        public Zapfhahn GetZapfhahnOfFuelType(FuelType requestedFuelType)
        {
            return this.zapfhaehne.Find(x => x.Equals(requestedFuelType));
        }

        public void GetFuelFromSelectedZapfhahn()
        {
            // if it isnt locked
            if (!currentlySelectedZapfhahn.IsLocked())
            {
                // get fuel from tank
            } else
            {
                // error message tank is locked
                Console.WriteLine("Warning: Zapfhahn is currently locked");
            }
        }




       





        // unlocks all zapfhaehne for after purchasing fuel
        public void UnlockAllZapfhaehne() 
        {
            this.zapfhaehne.ForEach(x => x.UnlockZapfhahn());
         
        }
      

        public void LockAllZapfhaehne()
        {
            this.zapfhaehne.ForEach(x => x.LockZapfhahn());
        }















        // TODO: need Rework
        /// <summary>
        /// Select a Zapfhahn by getting the first Zapfhahn that has the FUeltype requested
        /// </summary>
        /// <param name="fuelType"></param>
        //TODO: also check if the Zapfhahn inst used by another person
        //public void SelectZapfhahn(FuelType fuelType) 
        //{
        //    currentlySelectedZapfhahn = zapfhaene.Find(x => x.GetFuelTank().GetFuelType() == fuelType);

        //}


        //// Starts tanking from currently selected Zapfhahn
        //public void StartTanking(int amntOfFuel) 
        //{
        //    // was passiert mit dem output aus RemoveFromFuelTank?
        //    if(this.currentlySelectedZapfhahn != null) { 
        //        this.currentlySelectedZapfhahn.RemoveFromFuelTank(amntOfFuel);
        //    }
        //}
    }
}
