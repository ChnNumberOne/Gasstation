using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    class Zapfsaeule
    {

        public Zapfsaeule(List<FuelTank> fuelTanks)
        {
          
            foreach (FuelTank ft in fuelTanks) 
            {
                Zapfhahn zapfhahn = new Zapfhahn(ft);
                this.zapfhaene.Add(zapfhahn);
            }
        }

        private List<Zapfhahn> zapfhaene = new List<Zapfhahn>();

        private Zapfhahn currentlySelectedZapfhahn;


        /// <summary>
        /// Select a Zapfhahn by getting the first Zapfhahn that has the FUeltype requested
        /// </summary>
        /// <param name="fuelType"></param>
        //TODO: also check if the Zapfhahn inst used by another person
        public void SelectZapfhahn(FuelType fuelType) 
        {
            currentlySelectedZapfhahn = zapfhaene.Find(x => x.GetFuelTank().GetFuelType() == fuelType);
         
        }


        // Starts tanking from currently selected Zapfhahn
        public void StartTanking(int amntOfFuel) 
        {
            // was passiert mit dem output aus RemoveFromFuelTank?
            if(this.currentlySelectedZapfhahn != null) { 
                this.currentlySelectedZapfhahn.RemoveFromFuelTank(amntOfFuel);
            }
        }

       

        // unlocks all zapfhaehne for after purchasing fuel
        public void UnlockAllZapfhaehne() 
        {
            this.zapfhaene.ForEach(x => x.UnlockZapfhahn());
         
        }
      

        public void LockAllZapfhaehne()
        {
            this.zapfhaene.ForEach(x => x.LockZapfhahn());
        }
     
    }
}
