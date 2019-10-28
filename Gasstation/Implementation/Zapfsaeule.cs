using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    class Zapfsaeule
    {

        // Sieht nicht schlecht aus aber:
        // Deutsch Englisch nicht mixen
        // Gewisse List Suchen können deutlich vereinfacht werden (und sind damit wahrscheinlich auch besser für die Performance)

        public Zapfsaeule(List<FuelTank> fuelTanks)
        {
            //this.fuelTanks = fuelTanks;

            // creates new zapfhaehne and adds them to a list
            foreach (FuelTank ft in fuelTanks) 
            {
                Zapfhahn zapfhahn = new Zapfhahn(ft);
                zapfhaene.Add(zapfhahn);
            }
        }

        private List<FuelTank> fuelTanks = new List<FuelTank>();
        private List<Zapfhahn> zapfhaene = new List<Zapfhahn>();

        private Zapfhahn currentlySelectedZapfhahn;


        private FuelTank selectedFuelTank;

        public void SelectZapfhahn(FuelType fuelType, int fuel) 
        {
            // Arrow Function Nach Beispiel Kassenautomat (List.Find() with custom condition)
            foreach (Zapfhahn zpf in zapfhaene) 
            {
                if (fuelType == zpf.GetFuelTank().GetFuelType()) 
                {
                    currentlySelectedZapfhahn = zpf;
                }
                else 
                {
                    zpf.LockZapfhahn();
                }
            }
        }

        public void StartTanking(int amntOfFuel) 
        {
            // wieso entfernen wir den Zapfhahn wenn wir tanken mit dem wir tanken?
            currentlySelectedZapfhahn.removeFromFuelTank(amntOfFuel);
        }

        public void FinishTanking() 
        {
         
            int drainedFuel = currentlySelectedZapfhahn.FinishFueling();
            
        }

        public void UnlockAllZapfhaehne() 
        {
            foreach (Zapfhahn zpf in zapfhaene) 
            {
                zpf.UnlockZapfhahn();
            }
        }
      


        public void SelectFuelType(string fuelTypeName)
        {

            // Kassenautomat Beispiel wie oben List.Functions können das hier einfacher lösen
            // fehlt hier ein Field?
            foreach(FuelTank currentFuelTank in fuelTanks)
            {
                FuelType currentFuelType = currentFuelTank.GetFuelType();
               
                if (currentFuelType.GetFuelTypeName().Equals(fuelTypeName))
                {
                    this.selectedFuelTank = currentFuelTank;
                }
            }
        }

        // nested class Zapfhahn to access from within Zapfsaeule
        class Zapfhahn 
        {
            // bool for seeing if zapfhahn can be accessed
            private bool isLocked = false;

            // fuel tank that gets accessed by this zapfhahn
            private FuelTank fuelTank;

            // saves amount of total drained fuel
            private int drainedFuelTotal = 0;

            // constructor that creates a new fueltank
            public Zapfhahn(FuelType fuelType, int fuelCapacity)
            {
                fuelTank = new FuelTank(fuelType, fuelCapacity);
            }

            // constructor that uses an already created fueltank
            public Zapfhahn(FuelTank fuelTank) 
            {
                this.fuelTank = fuelTank;
            }

            // locks Zapfhahn
            public void LockZapfhahn() 
            {
                isLocked = true;
            }

            // unlocks Zapfhahn
            public void UnlockZapfhahn() 
            {
                isLocked = false;
            }

            // changes fueltank of Zapfhahn
            public void ChangeFuelTank(FuelTank fuelTank) 
            {
                this.fuelTank = fuelTank;
            }

            // removes certain amount of fuel from fueltank
            // was ist mit returns?
            public int removeFromFuelTank(int fuel) 
            {
                drainedFuelTotal += fuel;
                fuelTank.DrainFuelFromTank(fuel);
                return drainedFuelTotal;
            }

            // get total drained fuel and lock zapfhahn
            public int FinishFueling() 
            {
                int totalDrained = drainedFuelTotal;
                drainedFuelTotal = 0;
                isLocked = true;
                return totalDrained;
            }

            // returns fueltank
            public FuelTank GetFuelTank() 
            {
                return fuelTank;
            }
        }
    }
}
