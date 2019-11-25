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









        /// <summary>
        /// Creates a new Zapfsaeule and writes it to the Gasstation State
        /// creates its own Zapfhaehne for that purpose
        /// </summary>
        /// <param name="zapfhaehne"></param>
        /// <returns></returns>
        public static Zapfsaeule CreateZapfsaeule()
        {
            List<Zapfhahn> zapfhaehneForZapfseaule = new List<Zapfhahn>();
            foreach (FuelType ft in GasstationState.AvailableFuelTypes)
            {
                Zapfhahn zapfhahn = new Zapfhahn(ft.GetFuelTypeName());
                zapfhaehneForZapfseaule.Add(zapfhahn);
            }


            Zapfsaeule newCreatedZapfaseule = new Zapfsaeule(zapfhaehneForZapfseaule);
            GasstationState.AvailableZapfsaeulen.Add(newCreatedZapfaseule);
            return newCreatedZapfaseule;

        }


        



    }
}
