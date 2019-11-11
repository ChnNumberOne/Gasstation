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

        public static FuelType CreateFuelType(string fuelName, int costPerLiterInCent)
        {
            return new FuelType(fuelName, costPerLiterInCent);
        }

        public static void createFuelTank(int fuelCapacity)
        {
            // gibt es den Fueltype schon?
            FuelType fuelType = CreateFuelType(fuelName, costPerLiterInCent);
            // wenn ja binde auf den bereits bestehenden

            // wenn nein erstelle neuen

            FuelTank fuelTank = new FuelTank(fuelType, fuelCapacity);
        }
        //TODO: da jeder Zapfhahn zu einem Fueltype gehören muss müssen wir die Fueltypes zuerst erstellen
        // wenn wir einen FuelType erstellen braucht dieser keine Vorbedingungen

        public static void createZapfhaehne()
        {
            foreach (FuelType ft in fuelTypes)
            {
                Zapfhahn zapfhahn = new Zapfhahn(tank indem FuelType = fueltype);
            }
        }


        // TODO: hier geben wir eine Liste von Zapfhaehnen in eine Zapfsaeule und erstellen eine neue Zapfsaeule
        // Die Zapfhaehne haben noch keinen FuelType oder Tank disere muss auch erstellt werden
        public static void createZapfsaeule(List<Zapfhahn> zapfhaehne)
        {
            Zapfsaeule zapfsaeule = new Zapfsaeule(zapfhaehne);

        }

    

    }
}
