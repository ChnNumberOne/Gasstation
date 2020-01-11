using Gasstation.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasstation.Interfaces
{
    public interface ITankstelle
    {

      

        IList<Zapfsaeule> GetAllZapfsauelen();

        void PumpGasFromZapfsauele(Zapfsaeule zapfsaeule, IFuelType fuelType);

        List<int> PayTransaction(Transaction billToPay, IList<int> insertedMoney);

        List<Transaction> GetTransactionList();

        IReadOnlyList<int> GetAvailableCoins();

        void SaveFuelTanks();

        List<FuelType> GetAvailableFuelTypes();

        List<FuelTank> GetAvailableFuelTanks();

        float GetYearStats();

        float GetMonthStats();

        float GetWeekStats();

        float GetTodaysMoneyStats();

        int GetTodaysLiterStats(FuelType fuelType);

        FuelTank FindFuelTank(string fuelType);

  
    }
}
