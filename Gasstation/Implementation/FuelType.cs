using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class FuelType : IFuelType
    {

        protected int costPerLiterInCent;

        protected string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public FuelType()
        {
            GasstationState.AvailableFuelTypes.Add(this);
        }

        public FuelType(string name)
        {
            this.Name = name;
            GasstationState.AvailableFuelTypes.Add(this);
        }


        public FuelType(string name, int fuelCapacity)
        {
            new FuelTank(this, fuelCapacity);
            GasstationState.AvailableFuelTypes.Add(this);
        }

        public int CostPerLiterInCent
        {
            get
            {
                return this.costPerLiterInCent;
            }
            set
            {
                this.costPerLiterInCent = value;
            }
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
