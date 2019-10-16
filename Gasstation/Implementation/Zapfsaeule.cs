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
            this.fuelTanks = fuelTanks;
        }

        private List<FuelTank> fuelTanks = new List<FuelTank>();

        private FuelTank selectedFuelTank;
      


        public void SelectFuelType(string fuelTypeName)
        {
            foreach(FuelTank currentFuelTank in fuelTanks)
            {
                FuelType currentFuelType = currentFuelTank.GetFuelType();
               
                if (currentFuelType.GetFuelTypeName().Equals(fuelTypeName))
                {
                    this.selectedFuelTank = currentFuelTank;
                }
            }
        }

    }
}
