using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace Gasstation.Implementation
{
    // Singleton Class
    [Serializable]
    class Config
    {

        private static Config configInstance = null;

        private List<FuelTank> fuelTanks;

        public static Config CreateInstance()
        {
            if(configInstance == null)
            {
                configInstance = new Config();
            }
            return configInstance;
        }

        private Config()
        {

        }

        public void SetConfigurationData(List<FuelTank> fuelTanks)
        {
            this.fuelTanks = fuelTanks;
        }
        

        public void SaveConfig()
        {
            IFormatter formatterSave = new BinaryFormatter();
            Stream streamSave = new FileStream("D:/Daten/test.config", FileMode.Create, FileAccess.Write, FileShare.None);
            formatterSave.Serialize(streamSave, this);
            streamSave.Close();
        }

        public void ReadConfig()
        {
            IFormatter formatterLoad = new BinaryFormatter();
            Stream streamLoad = new FileStream("D:/Daten/test.config", FileMode.Open, FileAccess.Read, FileShare.Read);
            Config output = (Config) formatterLoad.Deserialize(streamLoad);
            this.fuelTanks = output.fuelTanks;
            foreach (FuelTank ft in this.fuelTanks)
            {
                
                Console.WriteLine(ft.GetFuelType());
            }
            streamLoad.Close();
            

        }
    }
}
