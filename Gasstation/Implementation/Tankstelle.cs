using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    public class Tankstelle
    {

        // Singleton
        private static Tankstelle currentInstance;

        public static Tankstelle Current()
        {
            return currentInstance ?? (currentInstance = new Tankstelle());
        }

        public List<Zapfsaeule> AvailableZapfsaeulen = new List<Zapfsaeule>();

        // Konstruktor mit Basiswerten Initialisierung
        private Tankstelle()
        {
            // Erstellen der FuelTypes und zuweisen auf State
            GasstationState.AvailableFuelTypes = new List<FuelType>
            {
                new Benzin(),
                new Diesel(),
                new Bleifrei(),
            };

            // Für jeden Fueltype einen FuelTank erstellen 
            IEnumerable<FuelTank> fuelTanks = GasstationState.AvailableFuelTypes.Select(fuelType => new FuelTank(fuelType, 1000));
            GasstationState.AvailableFuelTanks.AddRange(fuelTanks);

            // Liste von Zapfsaeulen erstellen
            // für jede Zapfsaeule   ->
            // für jeden FuelType einen Zapfhahn erstellen
            IEnumerable<Zapfsaeule> zapfsauelen =
                Enumerable
                .Range(0, 5)
                .Select(x => {
                    IEnumerable<Zapfhahn> zapfhaehneFuerSaeule = GasstationState.AvailableFuelTypes.Select(fuelType => new Zapfhahn(fuelType));
                    return new Zapfsaeule(zapfhaehneFuerSaeule.ToList());
                });
            this.AvailableZapfsaeulen.AddRange(zapfsauelen);
        }

      
        // Zapfhaehne readonly zurückgeben
        public IReadOnlyCollection<Zapfsaeule> GetAllZapfsauelen()
        {

            return this.AvailableZapfsaeulen.AsReadOnly();
        }

        // Sprit Bezug
        public void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType, decimal amount)
        {
            // Lock selected Zapfsaeule
            zapfsaeule.LockUnused(fuelType);

            // wir brauchen hier ja den FuelTank u den Sprit zu beziehen. Gehen wir direkt auf den FuelTank von hier oder machen wir das durch die Objektkette?
            // TODO: FUCKING STUCK AGAIN!!
            // Decrease Gas From FuelType we Recieve by Amount

            // Create Transaction
            // Save the Transaction WHERE?
        }
    }
}
