using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    class Tankstelle
    {
        //List<Kasse>

        List<Zapfsaeule> zapfsaeulen;

        Zapfsaeule selectedZapfsaeule;

        public Tankstelle()
        {
            this.zapfsaeulen = new List<Zapfsaeule>();
        }


        /// <summary>
        /// Takes a Number for the Zapfsaeule the Customer wants
        /// </summary>
        /// <param name="zapfsaeulenNummer"></param>
        public void CustomerSelectZapfsauele(int zapfsaeulenNummer)
        {
            this.selectedZapfsaeule = this.zapfsaeulen[zapfsaeulenNummer];

        }

    }
}
