using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public static class Tankstelle
    {





        /// <summary>
        /// Takes a Number for the Zapfsaeule the Customer wants
        /// </summary>
        /// <param name="zapfsaeulenNummer"></param>
        public static Zapfsaeule CustomerSelectZapfsauele(int zapfsaeulenNummer)
        {
            Zapfsaeule selectedZapfsaeule = GasstationState.AvailableZapfsaeulen[zapfsaeulenNummer];
            return selectedZapfsaeule;

        }










    }
}
