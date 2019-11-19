using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_OOP_Testing.Klasses
{
    public class Treibstoff
    {
        public int id;
        public string Name { get; set; }
        public double AnzLiter { get; set; }
        public double MaxCapacity { get; set; }


        public Treibstoff()
        {
            id = SavedValues.TreibstoffID;
            SavedValues.TreibstoffID++;
        }

        public void LiterBeziehen(float liter)
        {
            if (AnzLiter > 0)
            {
                AnzLiter -= liter;
            }
        }

        public double LiterEinfuegen(float liter)
        {
            double theoreticalCanister = AnzLiter + liter;
            if (theoreticalCanister <= MaxCapacity)
            {
                AnzLiter = theoreticalCanister;
                return AnzLiter;
            }
            else
            {
                double uebrig = theoreticalCanister - MaxCapacity;
                AnzLiter = MaxCapacity;
                return uebrig;
            }
        }
    }
}
