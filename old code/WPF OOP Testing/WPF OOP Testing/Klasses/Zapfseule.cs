using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_OOP_Testing.Klasses
{
    public class Zapfseule
    {
        private int id;
        private bool istBesetzt;
        public ObservableCollection<Treibstoff> Treibstoffe 
        {
            get; set;
        }
        public string Name { get; set; }

        public Zapfseule()
        {
            //id = ++newestID;
            id = SavedValues.ZapfsaeuleID;
            SavedValues.ZapfsaeuleID++;
            Treibstoffe = new ObservableCollection<Treibstoff>();
        }

        public void AddFuelToList(Treibstoff treibstoff)
        {
            Treibstoffe.Add(treibstoff);
        }

        public void Besetzen()
        {
            istBesetzt = true;
        }
        
        public void Abschliessen()
        {
            istBesetzt = false;
        }
        public static Zapfseule FindById(Tankstelle tankstelle, int id)
        {
            foreach (Zapfseule zpf in tankstelle.Zapfseulen)
            {
                if (zpf.id == id)
                {
                    return zpf;
                }
            }
            return null;
        }
    }
}
