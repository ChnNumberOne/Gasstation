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

        public void SelectZapfhahn(FuelType fuelType) 
        {
            // [RESOLVED]
            // Arrow Function Nach Beispiel Kassenautomat (List.Find() with custom condition) 
            currentlySelectedZapfhahn = zapfhaene.Find(x => x.GetFuelTank.GetFuelType() == fuelType);
            /*
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
            }*/
        }


        // Starts tanking from currently selected Zapfhahn
        public void StartTanking(int amntOfFuel) 
        {
            currentlySelectedZapfhahn.RemoveFromFuelTank(amntOfFuel);
        }

        // [NOTE] func FinishTanking wird warscheinlich entfernt -Benji
        // [PS:] Siehe unten bei auskommentierter func FinishFueling() fuer mehr Details
        /*
        public void FinishTanking() 
        {
         
            int drainedFuel = currentlySelectedZapfhahn.FinishFueling();
            
        }*/

        // unlocks all zapfhaehne for after purchasing fuel
        public void UnlockAllZapfhaehne() 
        {
            zapfhaene.ForEach(x => x.UnlockZapfhahn());
            /*
            foreach (Zapfhahn zpf in zapfhaene) 
            {
                zpf.UnlockZapfhahn();
            }
            */
        }
      


        public void SelectFuelType(FuelType fuelType)
        {

            // Kassenautomat Beispiel wie oben List.Functions können das hier einfacher lösen
            // fehlt hier ein Field?

            this.selectedFuelTank = fuelTanks.Find(x => x.GetFuelType() == fuelType);
            /*
            foreach(FuelTank currentFuelTank in fuelTanks)
            {
                FuelType currentFuelType = currentFuelTank.GetFuelType();
               
                if (currentFuelType.GetFuelTypeName().Equals(fuelTypeName))
                {
                    this.selectedFuelTank = currentFuelTank;
                }
            }*/
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
                this.isLocked = true;
            }

            // unlocks Zapfhahn
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

            //  [NOTE] func FinishFueling wird warscheinlich entfernt/nicht gebraucht. -Benji

            /*
             * @Thomas die Idee dahinter war, wenn ein Nutzer mehrere male hintereinander auftankt
             * (weil er nacher z.B merkt, dass es nicht genug fuel ist), das er mit FinishFueling()
             * "Bestaetigt", dass er nicht mehr tanken würde.
             * Dies war gedacht, um eine entgueltige Quittung zu erstellen.
             * 
             * Die Funktion wird warscheinlich eh nacher entfernt, hatte einfach gemeint zu erklaeren,
             * was die denkweise dahinter war.
             */

            // gibt bereits den total Drained by RemoveFromFuelTank zurück als return!
            // ist der rest wirklich nötig und wenn ja muss es hier sein?
            /*
            public int FinishFueling() 
            {
                int totalDrained = drainedFuelTotal;
                drainedFuelTotal = 0;
                this.isLocked = true;
                return totalDrained;
            }*/

            // returns fueltank (For information in Configuration for GUI)
            public FuelTank GetFuelTank() 
            {
                return fuelTank;
            }
        }
    }
}
