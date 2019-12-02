using System.Collections.Generic;

namespace Gasstation.Implementation
{
    public class Tankstelle
    {
        private static Tankstelle currentInstance;

        private Tankstelle()
        {
            // Erstellen der FuelTypes und zuweisen auf State
            GasstationState.AvailableFuelTypes = new List<FuelType>
            {
                new Benzin(),
                new Diesel(),
                new Bleifrei(),

            };

            // für jeden FuelType ein Tank erstellen und zuweisen auf State
            GasstationState.AvailableFuelTanks = new List<FuelTank>();
            foreach (FuelType fuelType in GasstationState.AvailableFuelTypes)
            {
                GasstationState.AvailableFuelTanks.Add(new FuelTank(fuelType, 1000));
            }

            // 5 Zapfsaeulen generieren
            for (int i = 0; i < 5; i++)
            {
                List<Zapfhahn> zapfhaehneFuerSaeule = new List<Zapfhahn>();
                foreach (FuelType fuelType in GasstationState.AvailableFuelTypes)
                {
                    zapfhaehneFuerSaeule.Add(new Zapfhahn(fuelType));
                }

                Zapfsaeule zapfsaeule = new Zapfsaeule(zapfhaehneFuerSaeule);
                GasstationState.AvailableZapfsaeulen.Add(zapfsaeule);
            }
        }

        public static Tankstelle Current()
        {
            return currentInstance ?? (currentInstance = new Tankstelle());
        }

        public IReadOnlyCollection<Zapfsaeule> GetAllZapfsauelen()
        {
            return new List<Zapfsaeule>().AsReadOnly();
        }

        public void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType, decimal amount)
        {
            // Lock

            // Decrese Gas
            // Create Transaction
        }

    }
}
