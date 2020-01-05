using Gasstation.Interfaces;
using Gasstation.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation.Implementation
{
    public class DataRepository : IDataRepository
    {
        private Settings settings = Settings.Default;

        public IReadOnlyList<Transaction> StoredTransactions
        {
            get
            {
                List<Transaction> data = settings.PaidTransactions ?? new List<Transaction>();
                return data.AsReadOnly();
            }

            set
            {
                settings.PaidTransactions = value.ToList();
                settings.Save();
            }
        }

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
          

    }
}
