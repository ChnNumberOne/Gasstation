using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Diesel : FuelType
    {

        public Diesel()
        {
            this.costPerLiterInCent = 145;
            this.name = "Diesel";
        }
     
    }
}
