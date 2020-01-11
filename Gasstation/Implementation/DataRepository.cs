//
//      author:             Thomas Fischer  t.fischer@siemens.com
//      date:               11/1/2020         
//      projectname:        tankstelle / Gasstation
//      version:            1.0
//      description:        a framework for a Gasstation application. 
//                          Based on a GUI on WPF Pages
//                          Warning this is an explorative code and may have instabilities dead code or wrong design decisions
//                          
//
//      class:              DataRepository
//      classDescription:   This class handles the Data Persistency via Properties because this is required for application settings as delivered by .NET


using Gasstation.Interfaces;
using Gasstation.Properties;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    /// <summary>
    /// Class for Interaction with the Settings File, allows reading and writing to the settings from the application
    /// </summary>
    public class DataRepository : IDataRepository
    {
        /// <summary>
        /// The Settings File
        /// </summary>
        private Settings settings = Settings.Default;

        /// <summary>
        /// Custom Property required for Setting Values and reading them from the Settings FIle
        /// This is the Lsit of Stored Transactions including unpaid and paid ones
        /// </summary>
        public IReadOnlyList<Transaction> StoredTransactions
        {
            get
            {
                List<Transaction> data = settings.Transactions ?? new List<Transaction>();
                // readonly so there is no access directly to the config file from the application itself
                return data.AsReadOnly();
            }

            set
            {
                settings.Transactions = value.ToList();
                settings.Save();
            }
        }

        /// <summary>
        /// Custom Property required for Setting Values and reading them from the Settings file
        /// This is the List of Stored FUelTanks so the fill level isnt loss on Programm restart
        /// </summary>
        public IReadOnlyList<FuelTank> StoredFuelTanks
        {
            get
            {
                List<FuelTank> fuelTanks = settings.FuelTanks ?? new List<FuelTank>();
              
                return fuelTanks;
            }
            set
            {
                settings.FuelTanks = value.ToList();
                settings.Save();
            }
        }
        
        /// <summary>
        /// Custom Property required for setting values and reading them from the settings file
        /// this is the list of stored cointype containers for the kassenautomat so the fill amount isnt lost on programm restart
        /// </summary>
        public IReadOnlyList<Container> StoredContainers
        {
            get
            {
                List<Container> containers = settings.Containers ?? new List<Container>();

                return containers;
            }
            set
            {
                settings.Containers = value.ToList();
                settings.Save();
            }
        }
    }
}
