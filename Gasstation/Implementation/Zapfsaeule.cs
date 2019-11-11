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
          
            foreach (FuelTank ft in fuelTanks) 
            {
                Zapfhahn zapfhahn = new Zapfhahn(ft);
                this.zapfhaene.Add(zapfhahn);
            }
        }

        private List<FuelTank> fuelTanks = new List<FuelTank>();
        private List<Zapfhahn> zapfhaene = new List<Zapfhahn>();

        private Zapfhahn currentlySelectedZapfhahn;


        private FuelTank selectedFuelTank;

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
      
        public void SelectFuelType(FuelType fuelType)
        {

            this.selectedFuelTank = fuelTanks.Find(x => x.GetFuelType() == fuelType);
        
        }

        // nested class Zapfhahn to access from within Zapfsaeule
     
    }
}
