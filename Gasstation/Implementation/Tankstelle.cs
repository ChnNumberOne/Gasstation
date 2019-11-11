using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public static class Tankstelle
    {
        //List<Kasse>

        static List<Zapfsaeule> zapfsaeulen = new List<Zapfsaeule>();

        static Zapfsaeule selectedZapfsaeule;



     

        /// <summary>
        /// Takes a Number for the Zapfsaeule the Customer wants
        /// </summary>
        /// <param name="zapfsaeulenNummer"></param>
        public static void CustomerSelectZapfsauele(int zapfsaeulenNummer)
        {
            selectedZapfsaeule = zapfsaeulen[zapfsaeulenNummer];

        }

        public static void createFuelTank(FuelType fuelType, int fuelCapacity)
        {
            FuelTank fuelTank = new FuelTank(fuelType, fuelCapacity);
        }
        

    

    }
}
