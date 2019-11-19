using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_OOP_Testing.Klasses
{
    public class SavedValues
    {
        public static ObservableCollection<Tankstelle> tankstellen
        {
            get; set;
        } = new ObservableCollection<Tankstelle>();
        public static int TankstellenID = 0;
        public static int ZapfsaeuleID = 0;
        public static int TreibstoffID = 0;

        public static Tankstelle FindTkstlByID(int id)
        {
            foreach (Tankstelle tankstelle in tankstellen)
            {
                if (tankstelle.id == id)
                {
                    return tankstelle;
                }
            }
            return null;
        }
    }

}
