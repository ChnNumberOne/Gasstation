using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public abstract class FuelType : IFuelType
    {

        protected int costPerLiterInCent;

        protected string name;

        public FuelType()
        {
            GasstationState.AvailableFuelTypes.Add(this);
        }

        public int GetCostPerLiterInCent()
        {
            return this.costPerLiterInCent;
        }

        public string GetFuelTypeName()
        {
            return this.name;
        }

        public void SetCostPerLiterInCent(int newCostPerLiterInCent)
        {
            this.costPerLiterInCent = newCostPerLiterInCent;
        }

        
    }
}
