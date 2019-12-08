using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Zapfsaeule
    {

        public Zapfsaeule(List<Zapfhahn> zapfhaehne)
        {
            this.zapfhaehne = zapfhaehne;

        }

        private List<Zapfhahn> zapfhaehne;

        private Zapfhahn selectedZapfhahn;

        private int currentFuelTransaction = 0;


        public List<Zapfhahn> GetZapfhaene()
        {
            return zapfhaehne;
        }

        public void Selectzapfhahn(Zapfhahn requestedZapfhahn)
        {

            this.selectedZapfhahn = requestedZapfhahn;
        }

        public void LockUnused(IFuelType fuelType)
        {
            // Lockt jedesmal // Flag setzen?

            IEnumerable<Zapfhahn> zapfhaehneToLock = zapfhaehne.FindAll(x => fuelType != x.GetFuelType()).ToList();
            foreach(Zapfhahn zapfhahnToLock in zapfhaehneToLock)
            {
                zapfhahnToLock.Lock();
            }

            
            
        }

        public void Unlock()
        {
            // Entsperren aller Zapfhaehne
        }

        public void GetFuelOfType(FuelType fueltype)
        {
            Zapfhahn zapfhahn = zapfhaehne.Find(x => fueltype == x.GetFuelType());
        
        }

        
      

















        public int GetCurrentFuelTransaction()
        {
            return this.currentFuelTransaction;
        }











    }
}
