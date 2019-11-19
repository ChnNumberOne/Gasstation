using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_OOP_Testing.Klasses
{
    public class Tankstelle
    {
        public static int newestId = 0;
        public int id { get; set; }
        public ObservableCollection<Zapfseule> Zapfseulen
        {
            get; set;
        }

        public ObservableCollection<Treibstoff> Treibstoff
        {
            get; set;
        }

        public string Name { get; set; }
        public Tankstelle(string name)
        {
            id = SavedValues.ZapfsaeuleID;
            SavedValues.ZapfsaeuleID++;
            Zapfseulen = new ObservableCollection<Zapfseule>();
            Treibstoff = new ObservableCollection<Treibstoff>();
            Name = name;
        }

        //finds object by id
        public static Tankstelle FindTankstelle(int id)
        {
            foreach (Tankstelle tk in SavedValues.tankstellen)
            {
                if (tk.id == id)
                {
                    return tk;
                }
            }
            return null;
        }
    }
}
