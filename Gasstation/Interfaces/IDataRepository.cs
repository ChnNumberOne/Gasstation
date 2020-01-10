using Gasstation.Implementation;
using System.Collections.Generic;

namespace Gasstation.Interfaces
{
    public interface IDataRepository
    {
        IReadOnlyList<Transaction> StoredTransactions { get; set; }

        IReadOnlyList<FuelTank> StoredFuelTanks { get; set; }

        IReadOnlyList<Container> StoredContainers { get; set; }
    }
}
