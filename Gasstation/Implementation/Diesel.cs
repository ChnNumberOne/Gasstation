using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Diesel : IFuelType
    {

        private int costPerLiterInCent;


        public int GetCostPerLiterInCent()
        {
            return this.costPerLiterInCent;
        }

        public string GetFuelTypeName()
        {
            return "Diesel";
        }

        public void SetCostPerLiterInCent(int newCostPerLiterInCent)
        {
            this.costPerLiterInCent = newCostPerLiterInCent;
        }
    }
}
