using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Gasstation.Implementation;

namespace Gasstation.Testfolder
{
    class TankstellenKonfiguration
    {
        public TankstellenKonfiguration(List<FuelType> fuelTypes, int anzahlZapfsaeulen) 
        {
            this.fuelTypes = fuelTypes;
            this.anzahlZapfsaeulen = anzahlZapfsaeulen;

            // TODO: konstante machen
            this.fileName = "config.txt";
        }
        private string fileName;

        private List<FuelType> fuelTypes;

        private int anzahlZapfsaeulen;

        public void SaveConfig()
        {
            FileStream fs = new FileStream(this.fileName, FileMode.Create);
            IFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, this.fuelTypes);
            fs.Position = 0;

            List<FuelType> readFuelTypes = (List<FuelType>)bf.Deserialize(fs);
            MessageBox.Show(readFuelTypes[0].GetCostPerLiterInCent().ToString());
            MessageBox.Show(this.fuelTypes[0].GetCostPerLiterInCent().ToString());
            fs.Close();
        }

    }
}
