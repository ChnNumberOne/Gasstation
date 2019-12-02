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

        private Zapfhahn selectedZapfhahn;

        private int currentFuelTransaction = 0;


        public List<Zapfhahn> GetZapfhaene() 
        {
            return zapfhaehne;
        }

        public void Selectzapfhahn(Zapfhahn requestedZapfhahn)
        {

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


        //public void RequestFuelFromZapfhahn(int fuelAmount)
        //{

        //    if (selectedZapfhahn != null)
        //    {
        //        // kann nur aufgerufen werden wenn er nicht gelockt ist.
        //        Console.WriteLine("not null");
        //        if (!selectedZapfhahn.IsLocked())
        //        {
                   
        //            Console.WriteLine("not locked");
        //            // alle bis auf diesen locken
        //            LockAllZapfhaehne();
        //            selectedZapfhahn.UnlockZapfhahn();

        //            // fuel beziehen
        //            this.currentFuelTransaction += selectedZapfhahn.DrainFuelFromTank(fuelAmount);


        //        } else
        //        {
        //            Console.WriteLine("Access Denied Zapfhahn locked");
        //        }
               
                

        //    }
            

        //}

        //public Transaction CreateTransaction()
        //{
        //    // again clean this with f
        //    Console.WriteLine("Transaction created");
        //    return new Transaction(this.selectedZapfhahn.GetFuelTank().GetFuelType().GetCostPerLiterInCent(), this.currentFuelTransaction);
          
        //}


        public int GetCurrentFuelTransaction()
        {
            return this.currentFuelTransaction;
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
