using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gasstation.Implementation
{
    public class FuelType : IFuelType
    {
        [XmlAttribute]
        public int CostPerLiterInCent { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        public FuelType()
        {            
        }

        public FuelType(string name, int costPerLiterInCent)
        {
            this.Name = name;
            this.CostPerLiterInCent = costPerLiterInCent;
        }

        public int GetCostPerLiterInCent()
        {
            return this.CostPerLiterInCent;
        }

        public string GetFuelTypeName()
        {
            return this.Name;
        }

        public void SetCostPerLiterInCent(int newCostPerLiterInCent)
        {
            this.CostPerLiterInCent = newCostPerLiterInCent;
        }

        
    }
}
