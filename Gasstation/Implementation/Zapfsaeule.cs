using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Implementation
{
    public class Zapfsaeule : INotifyPropertyChanged
    {
        // Event for PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public Zapfsaeule(List<Zapfhahn> zapfhaehne)
        {
            //this.Zapfhaehne = new List<Zapfhahn>();
            this.Zapfhaehne = zapfhaehne;

        }

        private FuelType selectedFuelType;

        private int drainedAmountOfFuel;

        private List<Zapfhahn> zapfhaehne;
        public List<Zapfhahn> Zapfhaehne 
        {
            get
            {
                /*
                List<Zapfhahn> zpfs = new List<Zapfhahn>();
                foreach (Zapfhahn zpf in zapfhaehne)
                {
                    zpfs.Add(zpf);
                }
                return zpfs;*/
                return this.zapfhaehne;
            } 
            set
            {
                /*
                BindingList<Zapfhahn> zpfs = new BindingList<Zapfhahn>();
                foreach (Zapfhahn zpf in value)
                {
                    zpfs.Add(zpf);
                }
                zapfhaehne = zpfs;
                OnPropertyChanged("Zapfhaehne");*/
                this.zapfhaehne = value;
            }
        }


        public List<Zapfhahn> GetZapfhaene() 
        {
            return this.Zapfhaehne;
        }

        public Zapfhahn SelectZapfhahnOfFuelType(FuelType requestedFuelType)
        {

            Zapfhahn selectedZapfhahn = this.Zapfhaehne.Single(x => x.Equals(requestedFuelType));
            if (selectedZapfhahn != null)
            {
                this.LockAllZapfhaehne();
                selectedZapfhahn.UnlockZapfhahn();
            }
            return selectedZapfhahn;
        }





        // unlocks all zapfhaehne for after purchasing fuel
        public void UnlockAllZapfhaehne()
        {
            this.Zapfhaehne.ForEach(x => x.UnlockZapfhahn());

        }


        public void LockAllZapfhaehne()
        {
            this.Zapfhaehne.ForEach(x => x.LockZapfhahn());
        }


        public void RequestFuelFromZapfhahn(int fuelAmount, FuelType requestedFuelType)
        {
            // Zapfhahn auswählen und Treibstoff beziehen. bezogene Menge an Zapfsaeule geben
            Zapfhahn selectedZapfhahn = SelectZapfhahnOfFuelType(requestedFuelType);
      
            this.drainedAmountOfFuel = selectedZapfhahn.DrainFuelFromTank(fuelAmount);
            

        }





        // Alerts everytime a property is changed
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }







        // TODO: need Rework
        /// <summary>
        /// Select a Zapfhahn by getting the first Zapfhahn that has the FUeltype requested
        /// </summary>
        /// <param name="fuelType"></param>
        //TODO: also check if the Zapfhahn inst used by another person
        //public void SelectZapfhahn(FuelType fuelType) 
        //{
        //    currentlySelectedZapfhahn = zapfhaene.Find(x => x.GetFuelTank().GetFuelType() == fuelType);

        //}


        //// Starts tanking from currently selected Zapfhahn
        //public void StartTanking(int amntOfFuel) 
        //{
        //    // was passiert mit dem output aus RemoveFromFuelTank?
        //    if(this.currentlySelectedZapfhahn != null) { 
        //        this.currentlySelectedZapfhahn.RemoveFromFuelTank(amntOfFuel);
        //    }
        //}
    }
}
