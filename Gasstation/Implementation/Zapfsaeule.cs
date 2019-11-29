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

        private FuelType selectedFuelType;

        private int drainedAmountOfFuel;

        private List<Zapfhahn> zapfhaehne;

        private Zapfhahn selectedZapfhahn;


        public List<Zapfhahn> GetZapfhaene() 
        {
            return zapfhaehne;
        }

        public void Selectzapfhahn(Zapfhahn requestedZapfhahn)
        {

            //Zapfhahn selectedZapfhahn = this.zapfhaehne.Single(x => x.Equals(requestedFuelType));
            //if (selectedZapfhahn != null)
            //{
            //    this.LockAllZapfhaehne();
            //    selectedZapfhahn.UnlockZapfhahn();
            //}
            this.selectedZapfhahn = requestedZapfhahn;
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


        public void RequestFuelFromZapfhahn(int fuelAmount, FuelType requestedFuelType)
        {
            // Zapfhahn auswählen und Treibstoff beziehen. bezogene Menge an Zapfsaeule geben
            //Zapfhahn selectedZapfhahn = SelectZapfhahnOfFuelType(requestedFuelType);
      
            //this.drainedAmountOfFuel = selectedZapfhahn.DrainFuelFromTank(fuelAmount);
            

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
