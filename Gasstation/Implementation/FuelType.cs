using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    class FuelType
    {

        public FuelType(string name, int costPerLiterInCent)
        {
            this.name = name;
            this.costPerLiterInCent = costPerLiterInCent;
        }

        private string name;

        // generally the lowest possible currency in a country (cent, rappen, etc)
        private int costPerLiterInCent;

        public string GetFuelTypeName()
        {
            return this.name;
        }

        public int GetCostPerLiterInCent()
        {
            return this.costPerLiterInCent;
        }

        public void SetCostPerLiter(int newCostPerLiterInCent)
        {
            this.costPerLiterInCent = newCostPerLiterInCent;
        }

        


    }
}
