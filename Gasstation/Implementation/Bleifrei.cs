using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Bleifrei : FuelType
    {

        public Bleifrei()
        {
            this.costPerLiterInCent = 105;
            this.name = "Bleifrei";
        }
   
    }
}
